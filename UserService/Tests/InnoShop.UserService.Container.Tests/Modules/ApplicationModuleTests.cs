using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System.Reflection;

namespace InnoShop.UserService.Container.Tests.Modules;

public class ApplicationModuleTests
{
    private static WebApplicationBuilder CreateWebApplicationBuilder()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Configuration["Jwt:Issuer"] = "TestIssuer";
        builder.Configuration["Jwt:Audience"] = "TestAudience";
        builder.Configuration["Jwt:Key"] = "TestKey";

        var mockUserRepository = new Mock<IUserRepository>();
        builder.Services.AddScoped<IUserRepository>(_ => mockUserRepository.Object);

        return builder;
    }

    [Fact]
    public void LoadApplicationModule_ShouldRegisterRequiredServices()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        Assert.NotNull(services.GetService<IBaseComponent>());
        Assert.NotNull(services.GetService<IBaseService>());
    }

    [Fact]
    public void LoadApplicationModule_ShouldConfigureSwagger()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();

        // Assert
        var swaggerGenServiceDescriptor = builder.Services
            .FirstOrDefault(d => d.ServiceType == typeof(Swashbuckle.AspNetCore.Swagger.ISwaggerProvider));

        Assert.NotNull(swaggerGenServiceDescriptor);
    }

    [Fact]
    public void LoadApplicationModule_ShouldConfigureJwtAuthentication()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var authenticationScheme = services
            .GetRequiredService<Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider>()
            .GetDefaultAuthenticateSchemeAsync()
            .Result;

        Assert.Equal(JwtBearerDefaults.AuthenticationScheme, authenticationScheme?.Name);
    }

    [Fact]
    public void LoadApplicationModule_ShouldRegisterAllImplementationsOfIBaseComponent()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var assembly = Assembly.GetAssembly(typeof(IBaseComponent));
        var componentTypes = assembly!.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces()
                .Any(i => i.IsAssignableTo(typeof(IBaseComponent))));

        foreach (var componentType in componentTypes)
        {
            var service = services.GetService(componentType.GetInterfaces().First());
            Assert.NotNull(service);
            Assert.True(service is IBaseComponent);
        }
    }

    [Fact]
    public void LoadApplicationModule_ShouldRegisterAllImplementationsOfIBaseService()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var assembly = Assembly.GetAssembly(typeof(IBaseService));
        var serviceTypes = assembly!.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces()
                .Any(i => i.IsAssignableTo(typeof(IBaseService))));

        foreach (var serviceType in serviceTypes)
        {
            var service = services.GetService(serviceType.GetInterfaces().First());
            Assert.NotNull(service);
            Assert.True(service is IBaseService);
        }
    }

    [Fact]
    public void LoadApplicationModule_ShouldConfigureJwtTokenValidationParameters()
    {
        // Arrange
        var builder = CreateWebApplicationBuilder();

        // Act
        builder.LoadApplicationModule();
        var services = builder.Services.BuildServiceProvider();

        // Assert
        var jwtBearerOptions = services.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>()
            .Get(JwtBearerDefaults.AuthenticationScheme);

        var tokenValidationParameters = jwtBearerOptions.TokenValidationParameters;

        Assert.True(tokenValidationParameters.ValidateIssuer);
        Assert.True(tokenValidationParameters.ValidateAudience);
        Assert.True(tokenValidationParameters.ValidateLifetime);
        Assert.True(tokenValidationParameters.ValidateIssuerSigningKey);
        Assert.Equal(builder.Configuration["Jwt:Issuer"], tokenValidationParameters.ValidIssuer);
        Assert.Equal(builder.Configuration["Jwt:Audience"], tokenValidationParameters.ValidAudience);
        Assert.NotNull(tokenValidationParameters.IssuerSigningKey);
    }
}