using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Entities
{
    [Table("detalles_planilla")]
    public class DetallePlanillaEntity : BaseEntity
    {
        [Required]
        [Column("planilla_id")]
        public int PlanillaId { get; set; }

        [ForeignKey("PlanillaId")]
        public virtual PlanillaEntity Planilla { get; set; }

        [Required]
        [Column("empleado_id")]
        public int EmpleadoId { get; set; }

        [ForeignKey("EmpleadoId")]
        public virtual EmpleadoEntity Empleado { get; set; }

        [Column("salario_base")]
        public decimal SalarioBase { get; set; }

        [Column("horas_extra")]
        public decimal HorasExtra { get; set; }

        [Column("monto_horas_extra")]
        public decimal MontoHorasExtra { get; set; }

        [Column("bonificaciones")]
        public decimal Bonificaciones { get; set; }

        [Column("deducciones")]
        public decimal Deducciones { get; set; }

        [Column("salario_neto")]
        public decimal SalarioNeto { get; set; } 

        [StringLength(200)]
        [Column("comentarios")]
        public string Comentarios { get; set; }
    }
}