﻿using FluentValidation.AspNetCore;
using InnoShop.ProductService.Container;
using InnoShop.ProductService.WebApi.Middlewares;

namespace InnoShop.ProductService.WebApi;

public class Program
{
    private const string PolicyName = "AllowSpecificOrigin";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();

        builder.LoadDomainModule();
        builder.LoadApplicationModule();

        builder.Services.AddEndpointsApiExplorer();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    PolicyName,
                    corsPolicyBuilder =>
                    {
                        corsPolicyBuilder.WithOrigins("https://localhost:5174", "https://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        var app = builder.Build();

        app.Migrate();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(PolicyName);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}