using Microsoft.EntityFrameworkCore;
using ComisionApi.Models;

namespace ComisionApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<ReglaComision> ReglasComision { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asegúrate de que EF Core sepa que ReglaId es la clave primaria
            modelBuilder.Entity<ReglaComision>().HasKey(r => r.ReglaId);
        }
    }
}