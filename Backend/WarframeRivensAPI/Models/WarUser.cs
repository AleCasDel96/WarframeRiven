using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WarframeRivensAPI.Models
{
    public class WarUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nickname debe tener entre 3 y 50 caracteres.")]
        public string Nickname { get; set; }
        public string? WarframeNick { get; set; }
        public string? SteamId { get; set; }
        public string? Icono { get; set; }
        public ICollection<Riven>? Rivens { get; set; }
    }
}