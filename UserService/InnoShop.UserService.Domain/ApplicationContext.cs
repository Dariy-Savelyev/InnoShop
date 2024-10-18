using InnoShop.UserService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> builder)
        : base(builder)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}