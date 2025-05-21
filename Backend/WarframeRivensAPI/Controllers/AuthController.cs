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
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        #region Config
        private readonly UserManager<WarUser> _userManager;
        private readonly SignInManager<WarUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(
            UserManager<WarUser> userManager,
            SignInManager<WarUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        #endregion
    
        #region Register
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)] //Si se registra, devuelve 201, el front muestra un mensaje de éxito
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si hay un error en el modelo, devuelve 400
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
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
            return Created();
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)] //Si loguea, devuelve 200 con el token
        [ProducesResponseType(StatusCodes.Status203NonAuthoritative)] //Si no loguea, devuelve 203
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Si hay un error en el modelo, devuelve 400
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
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
        #endregion
    }
    #region DTO
        public class RegisterDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
            public string Password { get; set; }

            [Required]
            [MinLength(4, ErrorMessage = "El Nickname debe tener al menos 4 caracteres.")]
            public string Nickname { get; set; }
        }

        public class LoginDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
        }
        #endregion
}