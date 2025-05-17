using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        #region DTO
        public class OfertaDTO
        {
            public string IdRiven { get; set; }
            public int PrecioVenta { get; set; }
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
        #endregion

        #region Ver ofertas
        [HttpGet("VerOfertas")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOfertas()
        {
            var ofertas = await _context.Ofertas
                .Where(o => o.Disponibilidad)
                .Include(o => o.Riven)
                .Include(o => o.Vendedor)
                .ToListAsync();
            return Ok(ofertas);
        }

        [HttpGet("Oferta/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)] //Devuelve el riven
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<Oferta>> GetOferta(string id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null) { return NotFound(); }
            return Ok(oferta);
        }
        #endregion

        #region Mis Pujas/ofertas
        [HttpGet("MisOfertas")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
                    NombreRiven = o.Riven.Nombre,
                    Arma = o.Riven.Arma
                }).ToListAsync();
            return Ok(ofertas);
        }

        [HttpGet("Disponibilidad/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Oferta>>> SetDisponible(string rivenID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ofertas = await _context.Ofertas.Where(o => o.IdRiven == rivenID).FirstAsync();
            if (ofertas == null) { return NotFound(); }
            if (ofertas.IdVendedor != userId) { return Unauthorized(); }
            ofertas.Disponibilidad = !ofertas.Disponibilidad;
            try
            {
                _context.Entry(ofertas).State = EntityState.Modified;
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
        [HttpPost("Crear")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se actualiza
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede actualizar, devuelve 400
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<IActionResult> CrearOferta(string RivenId, OfertaDTO oferta)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var riven = await _context.Rivens.FindAsync(RivenId);
            if (riven == null) { return NotFound("El riven no existe."); }
            Oferta nuevaOferta = new Oferta
            {
                Id = Guid.NewGuid().ToString(),
                IdRiven = RivenId,
                IdVendedor = userId,
                PrecioVenta = oferta.PrecioVenta,
                Disponibilidad = true,
            };
            try
            {
                _context.Entry(oferta).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(RivenId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPut("Editar/{id}")]
        [Authorize]
        public async Task<ActionResult<Oferta>> EditarOferta(Oferta nuevo)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var antiguo = await _context.Ofertas.Where(o => o.IdRiven == nuevo.IdRiven && o.Disponibilidad) .FirstOrDefaultAsync();
            if (!antiguo.Disponibilidad)
            {
                return BadRequest("La oferta no está activa.");
            }
            try
            {
                _context.Ofertas.Update(nuevo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("GetOferta", new { id = nuevo.Id }, nuevo);
        }

        [HttpDelete("Eliminar/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se elimina
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede eliminar, devuelve 400
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es el dueño, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<IActionResult> DeleteOferta(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null) { return NotFound(); }
            if (oferta.IdVendedor != userId) { return Unauthorized(); }
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

        private bool OfertaExists(string id)
        {
            return _context.Ofertas.Any(e => e.Id == id);
        }
    }
}
