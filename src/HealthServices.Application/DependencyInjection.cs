using FluentValidation;
using HealthServices.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace HealthServices.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHealthServicesApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // configure EF Core
            services.AddDbContext<HealthServicesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("HealthServicesDbConnection"),
                    options =>
                    {
                        options.MigrationsAssembly(typeof(HealthServicesDbContext).Assembly.FullName);
                    })
            );

            // adds MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // adds all FluentValidation validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
