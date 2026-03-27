using System;
using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dtos.Planillas
{
    public class PlanillaCreateDto
    {
        [Required(ErrorMessage = "El periodo es requerido")]
        [StringLength(50)]
        public string Periodo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaPago { get; set; }
    }
}