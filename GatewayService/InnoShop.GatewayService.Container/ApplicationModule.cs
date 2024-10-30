using FluentValidation;
using InnoShop.GatewayService.Application.ComponentInterfaces;
using InnoShop.GatewayService.Application.ServiceInterfaces;
using InnoShop.ProductService.Application.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace InnoShop.GatewayService.Container;

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

        builder.Services.AddValidatorsFromAssemblies(new[]
        {
            typeof(ProductCreationValidator).Assembly
        });

        builder.Services.AddHttpClient();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gateway API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field.",
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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });

        return builder;
    }
}