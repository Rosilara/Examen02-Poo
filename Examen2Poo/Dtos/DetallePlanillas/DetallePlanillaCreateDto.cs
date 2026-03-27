using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dtos.DetallesPlanilla
{
    public class DetallePlanillaCreateDto
    {
        [Required]
        public int PlanillaId { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser positivo")]
        public decimal SalarioBase { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las horas extra deben ser positivas")]
        public decimal HorasExtra { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto de horas extra debe ser positivo")]
        public decimal MontoHorasExtra { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las bonificaciones deben ser positivas")]
        public decimal Bonificaciones { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Las deducciones deben ser positivas")]
        public decimal Deducciones { get; set; }

        public string Comentarios { get; set; }
    }
}