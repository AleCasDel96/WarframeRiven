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
    public class RivensController : ControllerBase
    {
        #region Config
        private readonly WarRivenContext _context;
        private readonly UserManager<WarUser> _userManager;
        public RivensController(
            WarRivenContext context,
            UserManager<WarUser> userManager) { 
            _context = context;
            _userManager = userManager;
        }
        #endregion

        #region Inventario
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Riven>>> Inventario()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Rivens.Where(r => r.IdPropietario == userId).ToListAsync();
        }
        #endregion

        #region Ver Riven
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)] //Devuelve el riven
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es del usuario, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<ActionResult<Riven>> VerRiven(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var riven = await _context.Rivens.FindAsync(id);
            if (riven == null) {  return NotFound(); }
            if (riven.IdPropietario != userId) { return Unauthorized(); }
            return Ok(riven);
        }
        #endregion

        #region Editar Riven
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se actualiza
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede actualizar, devuelve 400
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiven(string id, Riven riven)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (riven == null) { return NotFound(); }
            if (riven.IdPropietario != userId) { return Unauthorized(); }
            if (id != riven.Id) { return BadRequest(); }
            try
            {
                _context.Entry(riven).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RivenExists(id)) { return NotFound(); }
                else { throw; }
            }
            return NoContent();
        }
        #endregion

        #region Añadir Riven
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)] //Devuelve 201 si se crea con éxito
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si no se puede crear, devuelve 400
        [ProducesResponseType(StatusCodes.Status409Conflict)] //Si ya existe, devuelve 409
        public async Task<ActionResult<Riven>> PostRiven(Riven riven)
        {
            if (riven == null) { return BadRequest(); }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            riven.IdPropietario = userId;
            try
            {
                _context.Rivens.Add(riven);
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
        #endregion

        #region Eliminar Riven
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)] //Devuelve 204 si se elimina
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] //Si no es del usuario, devuelve 401
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Si no existe, devuelve 404
        public async Task<IActionResult> DeleteRiven(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var riven = await _context.Rivens.FindAsync(id);
            if (riven == null) { return NotFound(); }
            if (riven.IdPropietario != userId) { return Unauthorized(); }
            var ofertas = await _context.Ofertas.Where(o => o.IdRiven == riven.Id) .ToListAsync();
            try
            {
                _context.Ofertas.RemoveRange(ofertas);
                _context.Rivens.Remove(riven);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
            return NoContent();
        }
        #endregion

        private bool RivenExists(string id) { return _context.Rivens.Any(e => e.Id == id); }
    }
}
