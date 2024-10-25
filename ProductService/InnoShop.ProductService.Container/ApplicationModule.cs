using FluentValidation;
using InnoShop.ProductService.Application.MapperProfiles;
using InnoShop.ProductService.Application.ServiceInterfaces;
using InnoShop.ProductService.Application.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InnoShop.ProductService.Container;

public static class ApplicationModule
{
    public static WebApplicationBuilder LoadApplicationModule(this WebApplicationBuilder builder)
    {
        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IBaseService>()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddAutoMapper(typeof(ProductProfile));

        builder.Services.AddValidatorsFromAssemblyContaining<ProductCreationValidator>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Product API",
                Version = "v1"
            });
        });

        builder.Services.AddAuthentication("GatewayAuth")
            .AddScheme<AuthenticationSchemeOptions, GatewayService.Services.AuthenticationService>("GatewayAuth", options =>
            {
                options.TimeProvider = TimeProvider.System;
            });

        return builder;
    }
}