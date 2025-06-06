﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarframeRivensAPI.Models;

namespace WarframeRivensAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserConfController : ControllerBase
    {
        private readonly UserManager<WarUser> _userManager;

        public UserConfController(UserManager<WarUser> userManager) { _userManager = userManager; }

        [HttpPut("ChangePass")]
        public async Task<IActionResult> CambiarPassword([FromBody] string newPassword)
        {

            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) return Unauthorized();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Contraseña actualizada.");
        }

        [HttpPut("ChangeNick")]
        public async Task<IActionResult> CambiarNickname([FromBody] string nickname)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) { return Unauthorized(); }
            user.Nickname = nickname;
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) { return BadRequest(result.Errors); }
                return Ok("Nickname actualizado.");
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("ChangeWarNick")]
        public async Task<IActionResult> CambiarWarframeNick([FromBody] string warframeNick)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) { return Unauthorized(); }
            user.WarframeNick = warframeNick;
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) { return BadRequest(result.Errors); }
                return Ok("WarframeNick actualizado.");
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("ChangeIcon")]
        public async Task<IActionResult> CambiarIcon([FromBody] string icon)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) { return Unauthorized(); }
            user.Icono = icon;
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) { return BadRequest(result.Errors); }
                return Ok("Icono actualizado.");
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("ChangeSteamId")]
        public async Task<IActionResult> CambiarSteamId([FromBody] string steamId)
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) { return Unauthorized(); }
            user.SteamId = steamId;
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) { return BadRequest(result.Errors); }
                return Ok("SteamId actualizado.");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("Upgrade")]
        [Authorize(Roles = "basic")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public async Task<IActionResult> Upgrade()
        {
            var mail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(mail);
            if (user == null) { return Unauthorized(); }
            try
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("confirmado") || roles.Contains("admin"))
                {
                    return Forbid();
                }
                await _userManager.AddToRoleAsync(user, "confirmado");
                return NoContent();
            }
            catch
            {
                throw;
            }
        }
    }
}