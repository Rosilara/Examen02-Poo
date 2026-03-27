using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("created_by_id")]
        public string CreatedById { get; set; }

        [Column("created_date")]
        public string CreatedDate { get; set; }

        [Column("updated_by_id")]
        public string UpdatedById { get; set; }

        [Column("updated_date")]
        public string UpdatedDate { get; set; }
    }
}