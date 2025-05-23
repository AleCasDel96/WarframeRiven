using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        #region Config
        private readonly WarRivenContext _context;
        private readonly UserManager<WarUser> _userManager;
        public OfertasController(WarRivenContext context, UserManager<WarUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        #region Ver ofertas
        [HttpGet("VerOfertas")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OfertaDTO>>> VerOfertas()
        {
            var ofertas = await _context.Ofertas
                .Where(o => o.Disponibilidad)
                .Include(o => o.Riven)
                .ThenInclude(r => r.Propietario)
                .Select(o => new OfertaDTO
                {
                    Id = o.Id,
                    IdRiven = o.IdRiven,
                    NombreRiven = o.Nombre,
                    Arma = o.Riven.Arma,
                    NickUsuario = o.Riven.Propietario.WarframeNick,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad
                })
                .ToListAsync();
            return Ok(ofertas);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)] //Devuelve el riven
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<OfertaDTO>> GetOferta(string id)
        {
            var oferta = await _context.Ofertas
                .Include(o => o.Riven)
                .ThenInclude(r => r.Propietario)
                .Where(o => o.Id == id)
                .Select(o => new OfertaDTO
                {
                    Id = o.Id,
                    IdRiven = o.IdRiven,
                    NombreRiven = o.Riven.Nombre,
                    Arma = o.Riven.Arma,
                    NickUsuario = o.Riven.Propietario.WarframeNick,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad
                })
                .FirstOrDefaultAsync();
            if (oferta == null) { return NotFound(); }
            return Ok(oferta);
        }
        #endregion

        #region Mis ofertas
        [HttpGet("MisOfertas")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OfertaDTO>>> GetMisOfertas()
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var ofertas = await _context.Ofertas
                .Include(o => o.Riven)
                .ThenInclude(r => r.Propietario)
                .Where(o => o.IdVendedor == user.Id)
                .Select(o => new OfertaDTO
                {
                    Id = o.Id,
                    IdRiven = o.IdRiven,
                    NombreRiven = o.Nombre,
                    Arma = o.Riven.Arma,
                    NickUsuario = o.Riven.Propietario.WarframeNick,
                    PrecioVenta = o.PrecioVenta,
                    Disponibilidad = o.Disponibilidad
                })
                .ToListAsync();
            return Ok(ofertas);
        }

        [HttpGet("Disponibilidad/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Oferta>>> SetDisponible(string id)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var ofertas = await _context.Ofertas.Where(o => o.IdRiven == id).FirstAsync();
            if (ofertas == null) { return NotFound(); }
            if (ofertas.IdVendedor != user.Id) { return Unauthorized(); }
            ofertas.Disponibilidad = !ofertas.Disponibilidad;
            try
            {
                _context.Ofertas.Update(ofertas);
                await _context.SaveChangesAsync();
                return Ok(ofertas);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        #endregion

        #region Añadir/Editar/Eliminar
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se actualiza
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede actualizar, devuelve 400
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<IActionResult> CrearOferta(NuevaOfertaDTO oferta)
        {
            Console.WriteLine("Crear Oferta");
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var riven = await _context.Rivens.FindAsync(oferta.IdRiven);
            if (riven == null) { return NotFound("El riven no existe."); }
            Oferta nuevaOferta = new Oferta
            {
                Id = Guid.NewGuid().ToString(),
                IdRiven = oferta.IdRiven,
                Nombre = riven.Arma + " " + riven.Nombre,
                IdVendedor = user.Id,
                PrecioVenta = oferta.PrecioVenta,
                Disponibilidad = true,
            };
            try
            {
                _context.Ofertas.Add(nuevaOferta);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("Editar/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status201Created)] //Devuelve 201 si se crea
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede crear, devuelve 400
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<Oferta>> EditarOferta(string id, int precio)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var oferta = await _context.Ofertas
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
            if (oferta == null) { return NotFound("La oferta no existe."); }
            if (oferta.IdVendedor != user.Id) { return Unauthorized(); }
            if (precio == 0) { return BadRequest("Precio no puede ser 0"); }
            oferta.PrecioVenta = precio;
            try
            {
                _context.Ofertas.Update(oferta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return CreatedAtAction("GetOferta", new { id = oferta.Id }, oferta);
        }

        [HttpDelete("Eliminar/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se elimina
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede eliminar, devuelve 400
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es el dueño, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<IActionResult> DeleteOferta(string id)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null) { return NotFound(); }
            if (oferta.IdVendedor != user.Id) { return Unauthorized(); }
            try
            {
                _context.Ofertas.Remove(oferta);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return BadRequest("No se puede eliminar la oferta.");
            }
        }
        #endregion
    }
    #region DTO
    public class OfertaDTO
    {
        public string Id { get; set; }
        public string IdRiven { get; set; }
        public string NombreRiven { get; set; }
        public string Arma { get; set; }
        public string NickUsuario { get; set; } // WarframeNick del propietario
        public int PrecioVenta { get; set; }
        public bool Disponibilidad { get; set; }
    }
    public class NuevaOfertaDTO
    {
        public string IdRiven { get; set; }
        public int PrecioVenta { get; set; }
    }
    #endregion
}