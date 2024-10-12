﻿using FluentValidation.AspNetCore;
using InnoShop.UserService.Container;
using InnoShop.UserService.CrossCutting.Extensions;
using InnoShop.UserService.WebApi.Middlewares;

namespace InnoShop.UserService.WebApi;

public class Program
{
    private const string PolicyName = "AllowSpecificOrigin";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddSignalR();

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
        app.Services.SeedRoles();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(PolicyName);

        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}