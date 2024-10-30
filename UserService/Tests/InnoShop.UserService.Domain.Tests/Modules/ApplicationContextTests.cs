using InnoShop.UserService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Domain.Tests.Modules;

public class ApplicationContextTests
{
    [Fact]
    public void ApplicationContext_ShouldHaveUsersDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        // Act
        using var context = new ApplicationContext(options);

        // Assert
        Assert.NotNull(context.Users);
    }

    [Fact]
    public void OnModelCreating_ShouldApplyConfigurations()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        // Act
        using var context = new ApplicationContext(options);
        var model = context.Model;

        // Assert
        var entityType = model.FindEntityType(typeof(User));
        Assert.NotNull(entityType);
    }
}