using InnoShop.UserService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InnoShop.UserService.Domain.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasMaxLength(36);

        builder.Property(p => p.UserName).HasMaxLength(255);

        builder.Property(p => p.Email).HasMaxLength(255);

        builder.Property(p => p.Role).HasMaxLength(255);

        builder.Property(p => p.PasswordHash).HasMaxLength(255);

        builder.HasIndex(x => new { x.UserName }).IsUnique();

        builder.HasIndex(x => new { x.Email }).IsUnique();
    }
}