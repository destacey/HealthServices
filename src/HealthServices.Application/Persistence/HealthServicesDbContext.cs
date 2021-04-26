using HealthServices.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HealthServices.Application.Persistence
{
    public class HealthServicesDbContext : DbContext
    {
        public HealthServicesDbContext(DbContextOptions<HealthServicesDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<Hospital> Hospitals { get; set; }
    }
}
