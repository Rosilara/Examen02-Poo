using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Entities
{
    [Table("empleados")]
    public class EmpleadoEntity : BaseEntity
    {
        [Required]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(10)]
        [Column("documento")]
        public string Documento { get; set; }

        [Column("fecha_contratacion")]
        public DateTime FechaContratacion { get; set; }

        [StringLength(80)]
        [Column("departamento")]
        public string Departamento { get; set; }

        [StringLength(80)]
        [Column("puesto_trabajo")]
        public string PuestoTrabajo { get; set; }

        [Column("salario_base")]
        public decimal SalarioBase { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true; 
    }
}