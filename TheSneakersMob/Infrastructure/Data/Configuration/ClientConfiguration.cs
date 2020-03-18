using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.UserId).IsRequired();
            builder.HasMany(c => c.Sells).WithOne(s => s.Seller);
            builder.HasMany(c => c.Buys).WithOne(b => b.Buyer);
            builder.HasMany(c => c.AuctionsCreated).WithOne(a => a.Auctioner);
            builder.HasMany(c => c.AuctionsWon).WithOne(a => a.Buyer);
        }
    }
}