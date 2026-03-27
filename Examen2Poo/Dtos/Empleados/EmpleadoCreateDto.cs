using System;
using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dtos.Empleados
{
    public class EmpleadoCreateDto
    {
         public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El documento es requerido")]
        [StringLength(20)]
        public string Documento { get; set; }

        public DateTime FechaContratacion { get; set; }
        public string Departamento { get; set; }
        public string PuestoTrabajo { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un numero positivo")]
        public decimal SalarioBase { get; set; }

        public bool Activo { get; set; }
    }
}