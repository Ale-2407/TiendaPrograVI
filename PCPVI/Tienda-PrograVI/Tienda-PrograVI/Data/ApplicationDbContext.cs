using Tienda_PrograVI.Models;
using Microsoft.EntityFrameworkCore;
namespace Tienda_PrograVI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto{ get; set; }
        public DbSet<Categoria> Categoria{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Producto)
                .HasForeignKey(p => p.Id_categoria);
        }

    }
}
