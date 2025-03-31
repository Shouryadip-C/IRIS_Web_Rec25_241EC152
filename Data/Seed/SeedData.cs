using IRIS_Web_Rec25_241EC152.Models;
using Microsoft.AspNetCore.Identity;

namespace IRIS_Web_Rec25_241EC152.Data.Seed
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles
            string[] roleNames = { "Admin", "Student" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user
            var adminEmail = "admin@nitk.edu.in";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin User",
                    IsAdmin = true,
                    Branch = "N/A"
                };

                await userManager.CreateAsync(adminUser, "Admin@1234");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            
            // Create test user 1
            var testUser1Email = "testuser1@nitk.edu.in";
            var testUser1 = await userManager.FindByEmailAsync(testUser1Email);
            if (testUser1 == null)
            {
                testUser1 = new ApplicationUser
                {
                    UserName = testUser1Email,
                    Email = testUser1Email,
                    FullName = "Test User 1",
                    IsAdmin = false,
                    Branch = "Artificial Intelligence"
                };

                await userManager.CreateAsync(testUser1, "Test@1234");
                await userManager.AddToRoleAsync(testUser1, "Student");
            }

            // Create test user 2
            var testUser2Email = "testuser2@nitk.edu.in";
            var testUser2 = await userManager.FindByEmailAsync(testUser2Email);
            if (testUser2 == null)
            {
                testUser2 = new ApplicationUser
                {
                    UserName = testUser2Email,
                    Email = testUser2Email,
                    FullName = "Test User 2",
                    IsAdmin = false,
                    Branch = "Artificial Intelligence"
                };

                await userManager.CreateAsync(testUser2, "Test@1234");
                await userManager.AddToRoleAsync(testUser2, "Student");
            }
            
        }
    }
}