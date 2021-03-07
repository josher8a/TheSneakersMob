using Microsoft.AspNetCore.Identity;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            SeedUsers(userManager,context);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var bob = "bob@test.com";
            var alice = "alice@test.com";

            //Seed test user bob
            if (userManager.FindByEmailAsync(bob).Result == null)
            {
                var user = new ApplicationUser {UserName = bob, Email = bob, FirstName = "Bob", LastName = "Test",Country = "Bolivia", EmailConfirmed = true};
                _ = userManager.CreateAsync(user, "Password123.").Result;
                var client = new Client(user.Id,user.UserName,user.Email,user.FirstName,user.LastName,user.Country, null);
                context.Add(client);
            }

            //Seed test user alice
            if (userManager.FindByEmailAsync(alice).Result == null)
            {
                var user = new ApplicationUser {UserName = alice, FirstName = "Alice", LastName = "Test",Country = "Spain", Email = alice, EmailConfirmed = true};
                _ = userManager.CreateAsync(user, "Password123.").Result;
                var client = new Client(user.Id,user.UserName,user.Email,user.FirstName,user.LastName,user.Country, null);
                context.Add(client);
            }
            context.SaveChanges();
        }
    }
}