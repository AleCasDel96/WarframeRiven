using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarframeRivensAPI.Data;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RivensController : ControllerBase
    {
        private readonly WarRivenContext _context;

        public RivensController(WarRivenContext context)
        {
            _context = context;
        }

        // GET: api/Rivens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Riven>>> GetRivens()
        {
            //solo los del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Rivens.Where(r=>r.IdPropietario==userId).ToListAsync();
        }

        // GET: api/Rivens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Riven>> GetRiven(string id)
        {
            //tiene que ser del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var riven = await _context.Rivens.FindAsync(id);

            if (riven == null)
            {
                return NotFound();
            }
            if (riven.IdPropietario != userId)
            {
                return Unauthorized();
            }


            return riven;
        }

        // PUT: api/Rivens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiven(string id, Riven riven)
        {
            //tiene que ser del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (riven == null)
            {
                return NotFound();
            }
            if (riven.IdPropietario != userId)
            {
                return Unauthorized();
            }

            if (id != riven.Id)
            {
                return BadRequest();
            }

            _context.Entry(riven).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RivenExists(id))
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

        // POST: api/Rivens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Riven>> PostRiven(Riven riven)
        {
            //tiene que ser para el usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            riven.IdPropietario = userId;
            _context.Rivens.Add(riven);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RivenExists(riven.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRiven", new { id = riven.Id }, riven);
        }

        // DELETE: api/Rivens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRiven(string id)
        {
            //tiene que ser del usuario logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var riven = await _context.Rivens.FindAsync(id);

            if (riven == null)
            {
                return NotFound();
            }

            if (riven.IdPropietario != userId)
            {
                return Unauthorized();
            }

            var ofertas = await _context.Ofertas
                .Where(o => o.IdRiven == riven.Id)
                .ToListAsync();

            _context.Ofertas.RemoveRange(ofertas);

            _context.Rivens.Remove(riven);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RivenExists(string id)
        {
            return _context.Rivens.Any(e => e.Id == id);
        }
    }
}
