using GelStock.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GelStock.Api.Data
{
    public class GelStockDbContext : DbContext
    {
        public GelStockDbContext(DbContextOptions<GelStockDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
