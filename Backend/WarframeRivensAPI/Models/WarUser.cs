using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WarframeRivensAPI.Models
{
    public class WarUser:IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "El Nickname debe tener entre 10 y 50 caracteres.")]
        public string Nickname { get; set; }

        
        public string? WarframeNick { get; set; }

        
        public string? SteamId { get; set; }

        
        public string? Icono { get; set; }

    }
}
