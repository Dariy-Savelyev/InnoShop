using InnoShop.UserService.Domain;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.UserService.Container.Tests.Modules;

public class DomainModuleTests
{
    private static WebApplicationBuilder CreateWebApplicationBuilder()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Configuration["ConnectionStrings_App"] = "Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;";

        return builder;
    }

    [Fact]
    public void LoadDomainModule_ShouldConfigureDbContext()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadDomainModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var dbContext = services.GetService<ApplicationContext>();
        Assert.NotNull(dbContext);
    }

    [Fact]
    public void LoadDomainModule_ShouldRegisterRepositories()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadDomainModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var repositories = builder.Services
            .Where(x => x.ServiceType.IsGenericType &&
                        x.ServiceType.GetGenericTypeDefinition() == typeof(IBaseRepository<,>))
            .ToList();

        Assert.NotEmpty(repositories);
    }

    [Fact]
    public void LoadDomainModule_ShouldConfigureRepositoriesWithScopedLifetime()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadDomainModule();

        // Assert
        var repositories = builder.Services
            .Where(x => x.ServiceType.IsGenericType &&
                        x.ServiceType.GetGenericTypeDefinition() == typeof(IBaseRepository<,>))
            .ToList();

        Assert.All(repositories, descriptor =>
            Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime));
    }
}