using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");
            builder.HasOne(l => l.Sell).WithMany(s => s.Likes).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(l => l.Auction).WithMany(s => s.Likes).OnDelete(DeleteBehavior.Cascade);
        }
    }
}