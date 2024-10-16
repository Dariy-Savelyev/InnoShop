using InnoShop.ProductService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductService.Domain;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> builder)
        : base(builder)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}