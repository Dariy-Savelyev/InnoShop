using FluentValidation;
using InnoShop.UserService.Application.ComponentInterfaces;
using InnoShop.UserService.Application.MapperProfiles;
using InnoShop.UserService.Application.ServiceInterfaces;
using InnoShop.UserService.Application.Validators;
using InnoShop.UserService.CrossCutting.Constants;
using InnoShop.UserService.Domain;
using InnoShop.UserService.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

        /*var builder1 = builder.Services.AddIdentityCore<User>(options =>
        {
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
            options.Lockout.MaxFailedAccessAttempts = 5;
        });
        var identityBuilder = new IdentityBuilder(builder1.UserType, builder1.Services);
        identityBuilder.AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddSignInManager<SignInManager<User>>();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 0;
        });*/

        /*var jwtTokenKey = builder.Configuration[ConfigurationConstants.JwtTokenKey];
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenKey!)),
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.Zero
        };

        builder.Services.AddSingleton(tokenValidationParameters);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });*/

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