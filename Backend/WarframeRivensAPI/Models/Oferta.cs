﻿using System.ComponentModel.DataAnnotations;

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
        public string Nombre { get; set; }
        public string IdVendedor { get; set; }
        public WarUser Vendedor { get; set; }
        [Required]
        public int PrecioVenta { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        public bool Disponibilidad { get; set; }

    }
}
