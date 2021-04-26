using FluentValidation.AspNetCore;
using HealthServices.Application;
using HealthServices.Application.Persistence;
using HealthServices.Application.Persistence.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HealthServices.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthServicesApplication(Configuration);
            services.AddControllersWithViews()
                .AddFluentValidation();

            services.AddHealthChecks()
                .AddDbContextCheck<HealthServicesDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // configure the database to auto migrate
                MigrateDatabase(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<HealthServicesDbContext>();
            context.Database.Migrate();

            if (context.Hospitals.Any() == false)
            {
                string json = File.ReadAllText(@"Data" + Path.DirectorySeparatorChar + "hospitals.json");
                var seedHospitals = JsonConvert.DeserializeObject<List<HospitalImport>>(json);

                context.Hospitals.AddRange(seedHospitals.Select(h => h.ToHospital()));
                context.SaveChanges();
            }
        }
    }
}
