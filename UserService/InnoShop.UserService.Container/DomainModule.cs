using InnoShop.UserService.Domain;
using InnoShop.UserService.Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoShop.UserService.Container;

public static class DomainModule
{
    public static WebApplicationBuilder LoadDomainModule(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings_App"], contextOptionsBuilder => contextOptionsBuilder.CommandTimeout(6000)));

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IRepository>()
            .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return builder;
    }
}