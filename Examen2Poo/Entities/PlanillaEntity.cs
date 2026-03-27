using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Entities
{
    [Table("planillas")]
    public class PlanillaEntity : BaseEntity
    {
        [Required]
        [StringLength(50)]
        [Column("periodo")]
        public string Periodo { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; }

        [StringLength(20)]
        [Column("estado")]
        public string Estado { get; set; } 

        public virtual ICollection<DetallePlanillaEntity> Detalles { get; set; }
    }
}