using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserManager<WarUser> _userManager;
        public TestController(WarRivenContext context, UserManager<WarUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("/")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TestDTO>>> Test()
        {
            TestDTO texto = new TestDTO
            {
                Mensaje = "La aplicación funciona, procede"
            };
            return Ok(texto);
        }
    }
    public class TestDTO
    {
        public string Mensaje { get; set; }
    }
}
