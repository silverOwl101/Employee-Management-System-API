using Employee_Management_System_API.Data;
using Employee_Management_System_API.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagementSystem.Test
{
    public class VirtualWebApplicationDB<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Clear default configs and reload
                config.Sources.Clear();

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);

                var settings = new Dictionary<string, string?>
                {
                    ["SeedSuperAdmin:SuperAdminGuid"] = Guid.NewGuid().ToString(),
                    ["SeedSuperAdmin:SuperAdminUsername"] = "sa",
                    ["SeedSuperAdmin:SuperAdminPassword"] = "company_Password101",
                    ["SeedSuperAdmin:SuperAdminEmail"] = "company@email.com"
                };

                config.AddInMemoryCollection(settings!);
            });

            builder.ConfigureServices(services =>
            {
                // Remove real DB context
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDBContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add in-memory DB for testing
                services.AddDbContext<ApplicationDBContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });

                // Build service provider and seed data
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDBContext>();

                    db.Database.EnsureCreated();
                    
                    DbSeeder.SeedSuperAdmin(scopedServices).Wait();
                }

            });
        }
    }
}
