using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MedicationPlatform.API
{
    public static class SeedData
    {
        public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webHost;
        }

        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<IdentityUser>>();
            await EnsureTestDoctorRoleAsync(userManager);
        }

        private static async Task EnsureTestDoctorRoleAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.Email == "nelu.tataru@med.com")
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new DoctorEntity
            {
                UserName = "nelu.tataru",
                FirstName = "Nelu",
                LastName = "Tataru", 
                Email = "nelu.tataru@med.com"
            };
            await userManager.CreateAsync(
                testAdmin, "Adm!n1");
            await userManager.AddToRoleAsync(
                testAdmin, Constants.DoctorRole);

            var testAdmin2 = await userManager.Users
                .Where(x => x.Email == "maria.pop@med.com")
                .SingleOrDefaultAsync();

            if (testAdmin2 != null) return;

            testAdmin = new DoctorEntity()
            {
                UserName = "maria.pop",
                FirstName = "Maria",
                LastName = "Pop",
                Email = "maria.pop@med.com"
            };
            await userManager.CreateAsync(
                testAdmin, "Adm!n2");
            await userManager.AddToRoleAsync(
                testAdmin, Constants.DoctorRole);
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.DoctorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.DoctorRole));
        }
    }
}
