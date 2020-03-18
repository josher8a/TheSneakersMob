using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.OwnsMany(p => p.Designers);
            builder.OwnsMany(p => p.Photos);
            builder.HasOne(p => p.Brand).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}