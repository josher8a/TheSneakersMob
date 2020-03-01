using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category{Id = 1, Name = "Tops"},
                new Category{Id = 2, Name = "Bottoms"},
                new Category{Id = 3, Name = "Outwear"},
                new Category{Id = 4, Name = "Boots"},
                new Category{Id = 5, Name = "Tailoring"},
                new Category{Id = 6, Name = "Accesories"}
            );
        }
    }
}