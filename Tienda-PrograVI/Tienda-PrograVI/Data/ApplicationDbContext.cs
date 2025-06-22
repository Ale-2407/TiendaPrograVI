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
    }
}
