using Microsoft.EntityFrameworkCore;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Asociado> Asociados { get; set; }
        public DbSet<Aumento> Aumentos { get; set; }
        public DbSet<HistorialSalario> HistorialSalarios { get; set; }


        // Agregar DbSet para AumentoDetalle (Consulta del procedimiento almacenado)
        public DbSet<AumentoDetalle> AumentoDetalles { get; set; }

        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir clave primaria para HistorialSalario
            modelBuilder.Entity<HistorialSalario>()
                .HasKey(hs => hs.HistorialID);

            // Configuración de precisión y escala para las propiedades decimal
            modelBuilder.Entity<Asociado>()
                .Property(a => a.Salario)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Asociado>()
                .Property(a => a.SalarioAnterior)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Aumento>()
                .Property(a => a.Porcentaje)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<HistorialSalario>()
                .Property(h => h.SalarioAnterior)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<HistorialSalario>()
                .Property(h => h.SalarioNuevo)
                .HasColumnType("decimal(18,2)");

            // Si AumentoDetalle es una vista o resultado de una consulta, no necesitas definir claves aquí
            modelBuilder.Entity<AumentoDetalle>().HasNoKey();
        }
    }
}
