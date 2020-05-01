using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class ClientFollowerConfiguration : IEntityTypeConfiguration<ClientFollower>
    {
        public void Configure(EntityTypeBuilder<ClientFollower> builder)
        {
            builder.HasKey(cf => new {cf.ClientId, cf.FollowerId});
            builder.HasOne(cf => cf.Client).WithMany(c => c.Followers).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(cf => cf.Follower).WithMany(c => c.Following);
        }
    }
}