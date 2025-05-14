using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<WarUser> _userManager;
        private readonly SignInManager<WarUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<WarUser> userManager,
        SignInManager<WarUser> signInManager, RoleManager<IdentityRole>
        roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public class RegisterRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
            public string Password { get; set; }

            [Required]
            [MinLength(10, ErrorMessage = "El Nickname debe tener al menos 10 caracteres.")]
            public string Nickname { get; set; }

        }

        public class LoginRequest
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest("El correo electrónico ya está en uso.");
            }

            var user = new WarUser
            {
                Email = model.Email,
                UserName = model.Email,
                Nickname = model.Nickname,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await _roleManager.RoleExistsAsync("basic"))
            {
                await _roleManager.CreateAsync(new IdentityRole("basic"));
            }
            await _userManager.AddToRoleAsync(user, "basic");

            return Ok(new { message = "Usuario registrado correctamente." });
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            // Generar token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName)
            };
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credencials
                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpGet("Upgrade")]
        [Authorize(Roles = "basic")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Upgrade()
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(user =>
                user.UserName == _userManager.GetUserId(User));
            if (user == null) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("premium") || roles.Contains("admin"))
            {
                return StatusCode(405);
            }

            await _userManager.AddToRoleAsync(user, "premium");

            return NoContent();
        }

        

    }
}
