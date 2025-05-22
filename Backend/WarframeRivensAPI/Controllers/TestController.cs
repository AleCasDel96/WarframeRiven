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
        public async Task<ActionResult<IEnumerable<Riven>>> Test()
        {
            var user = await _userManager.FindByEmailAsync("admin@warriven.com");
            Riven riven = new Riven
            {
                Arma = "Arma de prueba",
                Nombre = "Nombre de prueba",
                Polaridad = "Polaridad de prueba",
                Maestria = 10,
                Atrib1 = "Atributo 1 de prueba",
                Valor1 = 1.0m,
                Atrib2 = "Atributo 2 de prueba",
                Valor2 = 2.0m,
                Atrib3 = "Atributo 3 de prueba",
                Valor3 = 3.0m,
                DAtrib = "DAtributo de prueba",
                DValor = 4.0m,
                IdPropietario = user.Id
            };
            return Ok(riven);
        }
    }
}
