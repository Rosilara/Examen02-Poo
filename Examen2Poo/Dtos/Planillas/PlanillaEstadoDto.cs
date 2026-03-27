using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dtos.Planillas
{
    public class PlanillaEstadoDto
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public string Estado { get; set; } 
    }
}