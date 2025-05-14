using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarframeRivensAPI.Data;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly WarRivenContext _context;

        public OfertasController(WarRivenContext context)
        {
            _context = context;
        }

        public class OfertaDTO
        {
            public string Id { get; set; }
            public decimal PrecioVenta { get; set; }
            public bool Disponibilidad { get; set; }
            public string NombreRiven { get; set; }
            public string Arma { get; set; }
            public string NickUsuario { get; set; }
        }

        public class OfertaUsuarioDTO
        {
            public string Id { get; set; }
            public decimal PrecioVenta { get; set; }
            public bool Disponibilidad { get; set; }
            public bool Partida { get; set; }
            public bool Destino { get; set; }

            public string NombreRiven { get; set; }
            public string Arma { get; set; }
        }

        // GET: api/Ofertas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfertaDTO>>> GetOfertas()
        {
            var ofertas = await _context.Ofertas
                .Where(o => o.Disponibilidad)
                .Include(o => o.Riven)
                .Include(o => o.Comprador) 
                .Select(o => new OfertaDTO
                {
                    Id = o.Id,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad,
                    NombreRiven = o.Riven.Nombre,
                    Arma = o.Riven.Arma,
                    NickUsuario = string.IsNullOrEmpty(o.Comprador.WarframeNick) ? o.Comprador.Nickname : o.Comprador.WarframeNick
                })
                .ToListAsync();

            return Ok(ofertas);
        }

        // GET: api/Ofertas/mias
        [HttpGet("mias")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OfertaUsuarioDTO>>> GetMisOfertas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ofertas = await _context.Ofertas
                .Include(o => o.Riven)
                .Where(o => o.Riven.IdPropietario == userId)
                .Select(o => new OfertaUsuarioDTO
                {
                    Id = o.Id,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad,
                    Partida = o.Partida,
                    Destino = o.Destino,
                    NombreRiven = o.Riven.Nombre,
                    Arma = o.Riven.Arma
                })
                .ToListAsync();

            return Ok(ofertas);
        }

        [HttpGet("mispujas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OfertaUsuarioDTO>>> GetMisPujas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ofertas = await _context.Ofertas
                .Include(o => o.Riven)
                .Where(o => o.IdComprador == userId)
                .Select(o => new OfertaUsuarioDTO
                {
                    Id = o.Id,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad,
                    Partida = o.Partida,
                    Destino = o.Destino,
                    NombreRiven = o.Riven.Nombre,
                    Arma = o.Riven.Arma
                })
                .ToListAsync();

            return Ok(ofertas);
        }

        // GET: api/Ofertas/mias/5
        [HttpGet("open/{id}")]
        [Authorize]
        public async Task<IActionResult> CerrarOferta( string id)
        {
            // cambia el valor de disponivilidad a false de la oferta del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null || oferta.IdComprador != userId)
                return Unauthorized();

            oferta.Disponibilidad = false;
            await _context.SaveChangesAsync();

            return Ok(oferta);
        }

        // PUT: api/Ofertas/mias/5
        [HttpPut("open/{id}")]
        [Authorize]
        public async Task<IActionResult> OpenOferta(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null || oferta.IdComprador != userId)
                return Unauthorized();

            if (oferta.Destino || oferta.Partida)
                return BadRequest("No se puede editar una oferta ya en proceso.");

            oferta.Disponibilidad = true;
            await _context.SaveChangesAsync();

            return Ok("Oferta abierta.");
        }

        [HttpGet("ComfirVen/{id}")]
        [Authorize]
        public async Task<IActionResult> ComfirVenta(string id)
        {
            // cambia el valor de partida a true de la oferta del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null || oferta.IdComprador != userId)
                return Unauthorized();

            if (oferta.Disponibilidad != false)
                return BadRequest("La oferta debe estar cerrada para confirmar.");

            oferta.Partida = true;
            await _context.SaveChangesAsync();

            return Ok(oferta);
        }

        [HttpGet("ComfirCom/{id}")]
        [Authorize]
        public async Task<ActionResult<Oferta>> ComfirCompra(string id)
        {
            // cambia el valor de Destino a true de la oferta en curso
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
                return Unauthorized();

            if (oferta.Disponibilidad != false)
                return BadRequest("La oferta debe estar cerrada para confirmar.");

            oferta.Destino = true;
            await _context.SaveChangesAsync();

            return Ok(oferta);
        }

        [HttpGet("Transpaso/{id}")]
        [Authorize]
        public async Task<IActionResult> Transpaso(string id)
        {
            //comfirma que destino y partida estan a true
            //prepara la info de venta
            //cambia el propietario de el riven por el idComprador
            //hace post de venta y borra la oferta
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null || !(oferta.Partida && oferta.Destino))
                return BadRequest("Faltan confirmaciones");

            var riven = await _context.Rivens.FindAsync(oferta.IdRiven);
            if (riven == null)
                return NotFound("Riven no encontrado");

            var vendedorOriginal = riven.IdPropietario;
            riven.IdPropietario = oferta.IdComprador;

            var venta = new Venta
            {
                Id = Guid.NewGuid().ToString(),
                IdRiven = riven.Id,
                IdComprador = oferta.IdComprador,
                IdVendedor = vendedorOriginal,
                PrecioVenta = oferta.PrecioVenta,
                FechaVenta = DateTime.Now
            };

            _context.Ventas.Add(venta);
            _context.Ofertas.Remove(oferta);
            await _context.SaveChangesAsync();

            return Ok(venta);
        }

        // GET: api/Ofertas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOferta(string id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

        // PUT: api/Ofertas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutOferta(string id, Oferta oferta)
        {
            if (id != oferta.Id)
                return BadRequest();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ofertaOriginal = await _context.Ofertas.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            if (ofertaOriginal == null)
                return NotFound();

            if (ofertaOriginal.Destino || ofertaOriginal.Partida || !ofertaOriginal.Disponibilidad)
                return BadRequest("No se puede editar una oferta ya en proceso o cerrada.");

            // Buscar el riven asociado para obtener el propietario
            var riven = await _context.Rivens.FindAsync(ofertaOriginal.IdRiven);
            if (riven == null)
                return NotFound("El riven asociado a la oferta no existe.");

            // Validar que el nuevo precio sea mayor al actual
            if (oferta.PrecioVenta <= ofertaOriginal.PrecioVenta && userId != riven.IdPropietario)
            {
                return BadRequest("Solo el propietario del Riven puede reducir el precio de la oferta.");
            }

            // Mantener valores que no deben cambiar
            oferta.IdComprador = ofertaOriginal.IdComprador;
            oferta.IdRiven = ofertaOriginal.IdRiven;

            _context.Entry(oferta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Ofertas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Oferta>> PostOferta(Oferta oferta)
        {
            //tiene que ser del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            oferta.IdComprador = userId;
            oferta.Disponibilidad = false;
            oferta.Destino = false;
            oferta.Partida = false;

            bool yaOfertado = await _context.Ofertas.AnyAsync(o => o.IdRiven == oferta.IdRiven && o.Disponibilidad);

            if (yaOfertado)
            {
                return BadRequest("Ya existe una oferta activa para este Riven.");
            }

            _context.Ofertas.Add(oferta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfertaExists(oferta.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOferta", new { id = oferta.Id }, oferta);
        }

        // DELETE: api/Ofertas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOferta(string id)
        {
            //tiene que ser del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }
            if (oferta.IdComprador != userId)
                return Unauthorized();

            _context.Ofertas.Remove(oferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfertaExists(string id)
        {
            return _context.Ofertas.Any(e => e.Id == id);
        }
    }
}
