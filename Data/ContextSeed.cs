using Microsoft.AspNetCore.Identity;
using Co_Mute.Models;

namespace Co_Mute.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Driver.ToString()));
            

        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Caelan",
                Email = "s223031003@mandela.co.za",
                FirstName = "Caelan",
                LastName = "Longmore",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Driver.ToString());

                }

            }
        }
    }
}
