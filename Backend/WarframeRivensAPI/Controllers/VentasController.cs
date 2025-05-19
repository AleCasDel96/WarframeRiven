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
    public class VentasController : ControllerBase
    {
        #region Config
        private readonly WarRivenContext _context;
        private readonly UserManager<WarUser> _userManager;
        public VentasController(WarRivenContext context, UserManager<WarUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        #region DTO
        public class VentaDTO
        {
            public string IdVendedor { get; set; }
            public string IdComprador { get; set; }
            public decimal PrecioVenta { get; set; }
            public DateTime FechaVenta { get; set; }
        }
        #endregion

        #region Ver Ventas
        [HttpGet("MisVentas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ventas = await _context.Ventas.Include(v => v.Riven).Include(v => v.Vendedor).ToListAsync();
            return Ok(ventas);
        }
        [HttpGet("Venta/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)] //Devuelve el riven
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es del usuario, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<Venta>> GetVenta(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var venta = await _context.Ventas.Where(v => v.Id == id).Include(v => v.Riven).Include(v => v.Vendedor).Include(v => v.Comprador).FirstOrDefaultAsync();
            if (venta == null) { return NotFound(); }
            if (venta.IdVendedor != userId || venta.IdComprador != userId) { return Unauthorized(); }
            return Ok(venta);
        }
        #endregion

        #region Añadir ventas
        [HttpPost("Vendido")]
        public async Task<IActionResult> RealizarVenta(string ofertaId, string compradorId)
        {
            var oferta = await _context.Ofertas
                .Include(o => o.Riven)
                .Include(o => o.Vendedor)
                .FirstOrDefaultAsync(o => o.Id == ofertaId);
            if (oferta == null || !oferta.Disponibilidad) { return NotFound(); }
            var venta = new Venta
            {
                Id = Guid.NewGuid().ToString(),
                IdRiven = oferta.IdRiven,
                Riven = oferta.Riven,
                IdVendedor = oferta.IdVendedor,
                Vendedor = oferta.Vendedor,
                IdComprador = compradorId,
                Comprador = await _context.Users.FirstOrDefaultAsync(o => o.Id == compradorId),
                PrecioVenta = oferta.PrecioVenta,
                FechaVenta = DateTime.UtcNow,
                Finalizado = false
            };
            oferta.Disponibilidad = false;
            try
            {
                _context.Ventas.Add(venta);
                _context.Ofertas.Update(oferta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return NoContent();
        }

        [HttpPost("Confirmar")]
        public async Task<ActionResult<Venta>> ConfirmarVenta(Venta venta)
        {
            var riven = await _context.Rivens.FindAsync(venta.IdRiven);
            if (riven == null) { return NotFound(); }
            riven.IdPropietario = venta.IdComprador;
            venta.Finalizado = true;
            try
            {
                _context.Ventas.Update(venta);
                _context.Rivens.Update(riven);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return CreatedAtAction("GetVenta", new { id = venta.Id }, venta);
        }
        #endregion


    }
}