using FluentValidation;
using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.MapperProfiles;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.Application.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InnoShop.UserService.Container;

public static class ApplicationModule
{
    public static WebApplicationBuilder LoadApplicationModule(this WebApplicationBuilder builder)
    {
        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IBaseComponent>()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseComponent)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IBaseService>()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddAutoMapper(typeof(UserProfile));

        builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "User API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }
}