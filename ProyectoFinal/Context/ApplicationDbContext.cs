using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Context
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
       public DbSet<Medicamento> Medicamentos { get; set; }
       public DbSet<Laboratorio> Laboratorios { get; set; }
       public DbSet<Activo> Activos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicamento>()
                 .HasMany(m => m.Activos)
                 .WithMany(a => a.Medicamentos);

            base.OnModelCreating(modelBuilder);
        }
    }
}
