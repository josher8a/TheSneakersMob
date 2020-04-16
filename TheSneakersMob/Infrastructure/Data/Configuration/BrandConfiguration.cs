using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(
                new Brand{Id = 1, Name = "Supreme"},
                new Brand{Id = 2, Name = "Nike"},
                new Brand{Id = 3, Name = "Adidas"},
                new Brand{Id = 4, Name = "Jordan"},
                new Brand{Id = 5, Name = "Polo Ralph Lauren"},
                new Brand{Id = 6, Name = "Bape"},
                new Brand{Id = 7, Name = "Burberry"},
                new Brand{Id = 8, Name = "Champion"},
                new Brand{Id = 9, Name = "Tommy Hilfiger"},
                new Brand{Id = 10, Name = "Comme des Garcons"},
                new Brand{Id = 11, Name = "Undercover"},
                new Brand{Id = 12, Name = "Gucci"},
                new Brand{Id = 13, Name = "Uniqlo"},
                new Brand{Id = 14, Name = "The North Face"},
                new Brand{Id = 15, Name = "Palace"},
                new Brand{Id = 16, Name = "Anti Social Social Club"},
                new Brand{Id = 17, Name = "American Vintage"},
                new Brand{Id = 18, Name = "Off-White"},
                new Brand{Id = 19, Name = "Saint Laurent Paris"},
                new Brand{Id = 20, Name = "Stussy"},
                new Brand{Id = 21, Name = "LaurentMovie"},
                new Brand{Id = 22, Name = "Comme Des Garcons Homme Plus"},
                new Brand{Id = 23, Name = "Issey Miyake"},
                new Brand{Id = 24, Name = "Guess"},
                new Brand{Id = 25, Name = "Yohji Yamamoto"},
                new Brand{Id = 26, Name = "Louis Vuitton"},
                new Brand{Id = 27, Name = "Disney"},
                new Brand{Id = 28, Name = "Rare"},
                new Brand{Id = 29, Name = "Prada"},
                new Brand{Id = 30, Name = "Vans"},
                new Brand{Id = 31, Name = "Dior"},
                new Brand{Id = 32, Name = "Raf SimonsNumber "},
                new Brand{Id = 33, Name = "(N)ine"},
                new Brand{Id = 34, Name = "Fila"},
                new Brand{Id = 35, Name = "Maison Margiela "},
                new Brand{Id = 36, Name = "Givenchy"},
                new Brand{Id = 37, Name = "Cartoon Network"},
                new Brand{Id = 38, Name = "Hysteric Glamour"},
                new Brand{Id = 39, Name = "AcneStudios"},
                new Brand{Id = 40, Name = "Valentino"},
                new Brand{Id = 41, Name = "Balenciaga"},
                new Brand{Id = 42, Name = "Versace"},
                new Brand{Id = 43, Name = "Travis Scott"},
                new Brand{Id = 44, Name = "Reebok"},
                new Brand{Id = 45, Name = "Dolce & Gabbana"},
                new Brand{Id = 46, Name = "Stone Island"},
                new Brand{Id = 47, Name = "Kanye West"},
                new Brand{Id = 48, Name = "NFL"},
                new Brand{Id = 49, Name = "Kappa"},
                new Brand{Id = 50, Name = "RalphLauren"},
                new Brand{Id = 51, Name = "Converse"},
                new Brand{Id = 52, Name = "NBA"},
                new Brand{Id = 53, Name = "Lacoste"},
                new Brand{Id = 54, Name = "Puma"},
                new Brand{Id = 55, Name = "Harley Davidson"},
                new Brand{Id = 56, Name = "Nautica"},
                new Brand{Id = 57, Name = "Kith"},
                new Brand{Id = 58, Name = "Rick Owens"},
                new Brand{Id = 59, Name = "Carhartt"},
                new Brand{Id = 60, Name = "Calvin Klein"},
                new Brand{Id = 61, Name = "Kaws"},
                new Brand{Id = 62, Name = "Fendi "},
                new Brand{Id = 63, Name = "Kenzo"},
                new Brand{Id = 64, Name = "Visvim"},
                new Brand{Id = 65, Name = "A.P.C."},
                new Brand{Id = 66, Name = "Christian Dior"},
                new Brand{Id = 67, Name = "Monsieur"},
                new Brand{Id = 68, Name = "Gosha Rubchinskiy"},
                new Brand{Id = 69, Name = "Fear of God"},
                new Brand{Id = 71, Name = "Mickey Mouse"},
                new Brand{Id = 72, Name = "Billionaire Boys Club"},
                new Brand{Id = 73, Name = "Helmut Lang"},
                new Brand{Id = 74, Name = "Patagonia"},
                new Brand{Id = 75, Name = "MLB"},
                new Brand{Id = 76, Name = "Balmain"},
                new Brand{Id = 77, Name = "Playboy"},
                new Brand{Id = 78, Name = "Hanes"}
            );
        }
    }
}


