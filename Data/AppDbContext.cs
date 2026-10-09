using Microsoft.EntityFrameworkCore;
using csharp_rest_api.Models;

namespace csharp_rest_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeia o valor decimal para ser armazenado como double/real no SQLite
        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasConversion<double>();
    }
}