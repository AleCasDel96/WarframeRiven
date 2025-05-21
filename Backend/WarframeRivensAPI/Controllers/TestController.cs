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
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        private readonly WarRivenContext _context;
        public TestController(WarRivenContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Riven>>> Test()
        {
            return await _context.Rivens.ToListAsync();
        }

        [HttpGet("/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Riven>>> TestRiven()
        {
            string id = "AA6A8D4D-40E6-40B7-8046-BD0570CCD03A";
            var rivens = await _context.Rivens.Where(r => r.Id == id).ToListAsync();
            return Ok();
        }
    }
}
