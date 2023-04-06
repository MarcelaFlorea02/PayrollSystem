using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Persistence
{
    public class DataSeedingInit
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //create admin user
            var user = await userManager.FindByEmailAsync("admin@test.com");
            if (user == null)
            {
                IdentityUser u = new IdentityUser()
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };

                IdentityResult identityResult = await userManager.CreateAsync(u, "Password1");
                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(u, "Admin");
                }
            }
            //create manager user
            var userM = await userManager.FindByEmailAsync("manager@test.com");
            if (userM == null)
            {
                IdentityUser u = new IdentityUser()
                {
                    UserName = "manager@test.com",
                    Email = "manager@test.com"
                };

                IdentityResult identityResultM = await userManager.CreateAsync(u, "Password1");
                if (identityResultM.Succeeded)
                {
                    await userManager.AddToRoleAsync(u, "Manager");
                }
            }

            //create staff user
            var userS = await userManager.FindByEmailAsync("staff@test.com");
            if (userS == null)
            {
                IdentityUser u = new IdentityUser()
                {
                    UserName = "staff@test.com",
                    Email = "staff@test.com"
                };

                IdentityResult identityResultS = await userManager.CreateAsync(u, "Password1");
                if (identityResultS.Succeeded)
                {
                    await userManager.AddToRoleAsync(u, "Staff");
                }
            }


            //create no role user
            var userN = await userManager.FindByEmailAsync("john@test.com");
            if (userN == null)
            {
                IdentityUser u = new IdentityUser()
                {
                    UserName = "john@test.com",
                    Email = "john@test.com"
                };

                IdentityResult identityResultN = await userManager.CreateAsync(u, "Password1");
            }
        }
    }
}
