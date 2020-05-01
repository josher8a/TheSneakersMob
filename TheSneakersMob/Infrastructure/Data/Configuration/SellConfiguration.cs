using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class SellConfiguration : IEntityTypeConfiguration<Sell>
    {
        public void Configure(EntityTypeBuilder<Sell> builder)
        {
            builder.OwnsOne(s => s.Price);
            builder.OwnsOne(s => s.Feedback);
            builder.OwnsMany(s => s.HashTags);
        }
    }
}