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

        #region Ver Ventas
        [HttpGet("Historial")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetHistorial()
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var ventas = await _context.Ventas
                .Include(v => v.Riven)
                .Include(v => v.Vendedor)
                .Include(v => v.Comprador)
                .Where(v => v.Finalizado == true)
                .Where(v => v.IdVendedor == user.Id || v.IdComprador == user.Id)
                .Select(v => new VentaDTO
                {
                    Id = v.Id,
                    NombreRiven = v.Riven.Nombre,
                    Arma = v.Riven.Arma,
                    NickComprador = v.Comprador.Nickname,
                    NickVendedor = v.Vendedor.Nickname,
                    PrecioVenta = v.PrecioVenta,
                    FechaVenta = v.FechaVenta,
                    Finalizado = v.Finalizado
                })
                .ToListAsync();
            return Ok(ventas);
        }
        [HttpGet("MisVentas")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var ventas = await _context.Ventas
                .Include(v => v.Riven)
                .Include(v => v.Comprador)
                .Include(v => v.Vendedor)
                .Where(v => v.Finalizado == false)
                .Where(v => v.IdVendedor == user.Id)
                .Select(v => new VentaDTO
                {
                    Id = v.Id,
                    NombreRiven = v.Riven.Nombre,
                    Arma = v.Riven.Arma,
                    NickComprador = v.Comprador.Nickname,
                    NickVendedor = v.Vendedor.Nickname,
                    PrecioVenta = v.PrecioVenta,
                    FechaVenta = v.FechaVenta,
                    Finalizado = v.Finalizado
                })
                .ToListAsync();
            return Ok(ventas);
        }
        [HttpGet("Venta/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)] //Devuelve el riven
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es del usuario, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<Venta>> GetVenta(string id)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var venta = await _context.Ventas
                .Include(v => v.Riven)
                .Include(v => v.Vendedor)
                .Include(v => v.Comprador)
                .Where(v => v.Id == id)
                .FirstOrDefaultAsync();
            if (venta == null) { return NotFound(); }
            if (venta.IdVendedor != user.Id && venta.IdComprador != user.Id) { return Unauthorized(); }
            var dto = new VentaDTO
            {
                Id = venta.Id,
                NombreRiven = venta.Riven.Nombre,
                Arma = venta.Riven.Arma,
                NickComprador = venta.Comprador?.Nickname ?? "(desconocido)",
                NickVendedor = venta.Vendedor?.Nickname ?? "(desconocido)",
                PrecioVenta = venta.PrecioVenta,
                FechaVenta = venta.FechaVenta,
                Finalizado = venta.Finalizado
            };
            return Ok(dto);
        }
        #endregion

        #region Añadir ventas
        [HttpPost("Vendido")]
        public async Task<IActionResult> RealizarVenta(string ofertaId)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
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
                IdComprador = user.Id,
                Comprador = await _context.Users.FirstOrDefaultAsync(o => o.Id == user.Id),
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

        [HttpPut("Confirmar")]
        public async Task<ActionResult<Venta>> ConfirmarVenta(string id)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Id == id);
            if (venta.IdVendedor != user.Id) { return Unauthorized(); }
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

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Venta>> CancelarVenta(string id)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Id == id);
            if (venta.IdVendedor != user.Id) { return Unauthorized(); }
            try
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return NoContent();
        }
        #endregion
    }
    #region DTO
        public class VentaDTO
        {
            public string Id { get; set; }
            public string NombreRiven { get; set; }
            public string Arma { get; set; }
            public string NickComprador { get; set; }
            public string NickVendedor { get; set; }
            public int PrecioVenta { get; set; }
            public DateTime FechaVenta { get; set; }
            public bool Finalizado { get; set; }
        }
        #endregion
}