using Microsoft.AspNetCore.Identity;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager)
        {
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var bob = "bob@test.com";
            var alice = "alice@test.com";

            //Seed test user bob
            if (userManager.FindByEmailAsync(bob).Result == null)
            {
                var user = new ApplicationUser {UserName = bob, Email = bob, EmailConfirmed = true};
                _ = userManager.CreateAsync(user, "Password123.").Result;
            }

            //Seed test user alice
            if (userManager.FindByEmailAsync(alice).Result == null)
            {
                var user = new ApplicationUser {UserName = alice, Email = alice, EmailConfirmed = true};
                _ = userManager.CreateAsync(user, "Password123.").Result;
            }
        }
    }
}