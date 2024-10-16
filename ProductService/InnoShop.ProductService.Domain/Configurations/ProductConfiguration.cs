using InnoShop.ProductService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoShop.ProductService.Domain.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(255);

        builder.Property(p => p.Description).HasMaxLength(255);

        builder.Property(p => p.Price).HasColumnType("decimal(18, 2)");

        builder.Property(p => p.IsAvailable);

        builder.Property(p => p.CreationDate);
    }
}