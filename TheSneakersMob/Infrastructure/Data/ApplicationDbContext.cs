using System.Reflection;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Sell> Sells {get; set;}
        public DbSet<Auction> Auctions {get; set;}  
        public DbSet<Category> Categories {get; set;}
        public DbSet<SubCategory> SubCategories {get; set;}
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Client> Clients {get; set;}
        public ApplicationDbContext(
           DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }    
    }
}
