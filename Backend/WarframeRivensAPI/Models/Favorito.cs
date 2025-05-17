using System.ComponentModel.DataAnnotations;

namespace WarframeRivensAPI.Models
{
    public class Favorito
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string IdRiven { get; set; }
        public Riven Riven { get; set; }

        [Required]
        public string IdUser { get; set; }
        public WarUser User { get; set; }
    }
}
