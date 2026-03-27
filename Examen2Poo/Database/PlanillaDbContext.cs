using Microsoft.EntityFrameworkCore;
using Examen2Poo.Entities;

namespace Examen2Poo.Database
{
    public class PlanillaDbContext : DbContext
    {
        public PlanillaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EmpleadoEntity> Empleados { get; set; }
        public DbSet<PlanillaEntity> Planillas { get; set; }
        public DbSet<DetallePlanillaEntity> DetallesPlanilla { get; set; }
    }
}
