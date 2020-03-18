using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.OwnsOne(a => a.DirectBuyPrice);
            builder.OwnsOne(a => a.InitialPrize);
            builder.OwnsMany(a => a.HashTags);
            builder.OwnsMany(a => a.Bids, b => {
                b.OwnsOne(b => b.Amount);
            });
        }
    }
}