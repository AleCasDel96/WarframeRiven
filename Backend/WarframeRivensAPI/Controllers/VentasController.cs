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
    public class VentasController : ControllerBase
    {
        private readonly WarRivenContext _context;

        public VentasController(WarRivenContext context)
        {
            _context = context;
        }

        public class VentaDTO
        {
            public string Id { get; set; }
            public decimal PrecioVenta { get; set; }
            public DateTime FechaVenta { get; set; }

            public string NombreRiven { get; set; }
            public string Arma { get; set; }

            public string NickVendedor { get; set; }
            public string NickComprador { get; set; }
        }

        // GET: api/Ventas
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> GetVentas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ventas = await _context.Ventas
                .Include(v => v.Riven)
                .Include(v => v.Comprador)
                .Include(v => v.Vendedor)
                .Where(v => v.IdComprador == userId || v.IdVendedor == userId)
                .Select(v => new VentaDTO
                {
                    Id = v.Id,
                    PrecioVenta = v.PrecioVenta,
                    FechaVenta = v.FechaVenta,
                    NombreRiven = v.Riven.Nombre,
                    Arma = v.Riven.Arma,
                    NickComprador = string.IsNullOrEmpty(v.Comprador.WarframeNick)
                        ? v.Comprador.Nickname
                        : v.Comprador.WarframeNick,
                    NickVendedor = string.IsNullOrEmpty(v.Vendedor.WarframeNick)
                        ? v.Vendedor.Nickname
                        : v.Vendedor.WarframeNick
                })
                .ToListAsync();

            return Ok(ventas);
        }



        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(string id)
        {
            //tiene que ser del usuario logueado (comprador o vendedor)
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVenta(string id, Venta venta)
        //{
        //    if (id != venta.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(venta).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VentaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    venta.IdVendedor = userId;
        //    _context.Ventas.Add(venta);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (VentaExists(venta.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetVenta", new { id = venta.Id }, venta);
        //}

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(string id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaExists(string id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}
