using Directorio.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Directorio.Api.Data
{
    public class DirectorioDbContext : DbContext
    {
        public DirectorioDbContext(DbContextOptions<DirectorioDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Factura> Facturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();
            modelBuilder.Entity<Persona>()
                .HasMany(p => p.Facturas)
                .WithOne(f => f.Persona)
                .HasForeignKey(f => f.PersonaId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
