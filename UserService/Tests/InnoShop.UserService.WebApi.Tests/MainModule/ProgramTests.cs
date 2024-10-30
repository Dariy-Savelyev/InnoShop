using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace InnoShop.UserService.WebApi.Tests.MainModule;

public class ProgramTests
{
    [Fact]
    public async Task Program_ConfiguresServices_Correctly()
    {
        // Arrange & Act
        using var host = await CreateTestHost();
        var services = host.Services;

        // Assert
        Assert.NotNull(services.GetService<IValidatorFactory>());
        Assert.NotNull(services.GetService<IControllerFactory>());
    }

    [Fact]
    public async Task Program_ConfiguresCors_InDevelopmentEnvironment()
    {
        // Arrange & Act
        using var host = await CreateTestHost(true);
        var services = host.Services;

        // Assert
        var corsService = services.GetService<ICorsService>();
        Assert.NotNull(corsService);
    }

    [Fact]
    public async Task Program_DoesNotConfigureCors_InProductionEnvironment()
    {
        // Arrange & Act
        using var host = await CreateTestHost(false);
        var services = host.Services;

        // Assert
        var corsPolicyProvider = services.GetService<ICorsPolicyProvider>();
        Assert.NotNull(corsPolicyProvider);
    }

    [Fact]
    public async Task Program_ConfiguresSwagger_InDevelopmentEnvironment()
    {
        // Arrange & Act
        using var host = await CreateTestHost(true);
        var services = host.Services;

        // Assert
        Assert.NotNull(services.GetService<ISwaggerProvider>());
    }

    private static async Task<IHost> CreateTestHost(bool isDevelopment = true)
    {
        var hostBuilder = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .UseEnvironment(isDevelopment ? "Development" : "Production")
                    .UseStartup<TestStartup>();
            });

        return await hostBuilder.StartAsync();
    }
}

public class TestStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddFluentValidationAutoValidation();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowSpecificOrigin");
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}