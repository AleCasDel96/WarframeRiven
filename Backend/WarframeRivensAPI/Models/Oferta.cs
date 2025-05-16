using System.ComponentModel.DataAnnotations;

namespace WarframeRivensAPI.Models
{
    public class Oferta
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string IdRiven { get; set; }
        public Riven Riven { get; set; }

        [Required]
        public int PrecioVenta { get; set; }

        [Required]
        public string IdComprador { get; set; }
        public WarUser Comprador { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; }

        public bool Disponibilidad { get; set; }

        public bool Partida { get; set; }
        public bool Destino { get; set; }

    }
}
