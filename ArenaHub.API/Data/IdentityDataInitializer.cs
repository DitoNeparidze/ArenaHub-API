using Microsoft.AspNetCore.Identity;
using ArenaHub.API.Constants;
using ArenaHub.API.Entities;

namespace ArenaHub.API.Data
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = new[]
            {
                AppRoles.Admin,
                AppRoles.User
            };

            foreach (var roleName in roleNames)
            {
                if (!(await roleManager.RoleExistsAsync(roleName)))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = configuration["SeedAdmin:Email"];
            var adminPassword = configuration["SeedAdmin:Password"];

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail!);

            if (existingAdmin == null)
            {
                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail
                };
                var result = await userManager.CreateAsync(admin, adminPassword!);

                if (!result.Succeeded)
                {
                    var error = string.Join(", ", result.Errors.Select(r => r.Description));

                    throw new Exception($"Admin seeding failed: {error}");

                }

                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }
    }
}
