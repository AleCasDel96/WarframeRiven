using System.ComponentModel.DataAnnotations;

namespace WarframeRivensAPI.Models
{
    public class Riven
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Arma { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string IdPropietario { get; set; }
        public WarUser Propietario { get; set; }

        [Required]
        public string Atrib1 { get; set; }

        [Required]
        public decimal Valor1 { get; set; }

        public string? Atrib2 { get; set; }

        public decimal? Valor2 { get; set; }

        public string? Atrib3 { get; set; }

        public decimal? Valor3 { get; set; }

        public string? DAtrib { get; set; }

        public decimal? DValor { get; set; }


    }
}
