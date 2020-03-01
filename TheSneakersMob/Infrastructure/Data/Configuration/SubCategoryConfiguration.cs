using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
             builder.HasData(
                //Tops
                new SubCategory{Id = 1, CategoryId = 1, Name = "Long Sleeve"},
                new SubCategory{Id = 2, CategoryId = 1, Name = "T-Shirts"},
                new SubCategory{Id = 3, CategoryId = 1, Name = "Polos"},
                new SubCategory{Id = 4, CategoryId = 1, Name = "Shirts(Butoom Ups)"},
                new SubCategory{Id = 5, CategoryId = 1, Name = "Short Sleeve T-Shirts"},
                new SubCategory{Id = 6, CategoryId = 1, Name = "Sweaters & Knitwear"},
                new SubCategory{Id = 7, CategoryId = 1, Name = "Sweatshirts & Hoodies"},
                new SubCategory{Id = 8, CategoryId = 1, Name = "Tank Tops & Sleeveless"},
                new SubCategory{Id = 9, CategoryId = 1, Name = "Jerseys"},
                //Bottoms
                new SubCategory{Id = 100, CategoryId = 2, Name = "Casual Pants"},
                new SubCategory{Id = 101, CategoryId = 2, Name = "Cropped Pants"},
                new SubCategory{Id = 102, CategoryId = 2, Name = "Denim"},
                new SubCategory{Id = 103, CategoryId = 2, Name = "Leggings"},
                new SubCategory{Id = 104, CategoryId = 2, Name = "Overalls & Jumpsuits"},
                new SubCategory{Id = 105, CategoryId = 2, Name = "Shorts"},
                new SubCategory{Id = 106, CategoryId = 2, Name = "Sweatpants & Joggers"},
                new SubCategory{Id = 107, CategoryId = 2, Name = "Swimwear"},
                //Outwear
                new SubCategory{Id = 200, CategoryId = 3, Name = "Bombers"},
                new SubCategory{Id = 201, CategoryId = 3, Name = "Cloaks & Capes"},
                new SubCategory{Id = 202, CategoryId = 3, Name = "Denim Jackets"},
                new SubCategory{Id = 203, CategoryId = 3, Name = "Heavy Coats"},
                new SubCategory{Id = 204, CategoryId = 3, Name = "Leather Jackets"},
                new SubCategory{Id = 205, CategoryId = 3, Name = "Light Jackets"},
                new SubCategory{Id = 206, CategoryId = 3, Name = "Parkas"},
                new SubCategory{Id = 207, CategoryId = 3, Name = "Raincoats"},
                new SubCategory{Id = 208, CategoryId = 3, Name = "Vests"},
                //Boots
                new SubCategory{Id = 300, CategoryId = 4, Name = "Casual Leather Shoes"},
                new SubCategory{Id = 301, CategoryId = 4, Name = "Formal Shoes"},
                new SubCategory{Id = 302, CategoryId = 4, Name = "Hi-Top Sneakers"},
                new SubCategory{Id = 303, CategoryId = 4, Name = "Low-Top Sneakers"},
                new SubCategory{Id = 304, CategoryId = 4, Name = "Sandals"},
                new SubCategory{Id = 305, CategoryId = 4, Name = "Slip Ons"},
                //Tailoring
                new SubCategory{Id = 400, CategoryId = 5, Name = "Blazers"},
                new SubCategory{Id = 401, CategoryId = 5, Name = "Formal Shirting"},
                new SubCategory{Id = 402, CategoryId = 5, Name = "Formal Trousers"},
                new SubCategory{Id = 403, CategoryId = 5, Name = "Suits"},
                new SubCategory{Id = 404, CategoryId = 5, Name = "Tuxedos"},
                new SubCategory{Id = 405, CategoryId = 5, Name = "Vests"},
                //Accesories
                new SubCategory{Id = 500, CategoryId = 6, Name = "Bags & Luggage"},
                new SubCategory{Id = 501, CategoryId = 6, Name = "Belts"},
                new SubCategory{Id = 502, CategoryId = 6, Name = "Glasses"},
                new SubCategory{Id = 503, CategoryId = 6, Name = "Gloves & Scarves"},
                new SubCategory{Id = 504, CategoryId = 6, Name = "Hats"},
                new SubCategory{Id = 505, CategoryId = 6, Name = "Jewelry & Watches"},
                new SubCategory{Id = 506, CategoryId = 6, Name = "Wallets"},
                new SubCategory{Id = 507, CategoryId = 6, Name = "Miscellaneous"},
                new SubCategory{Id = 508, CategoryId = 6, Name = "Periodicals"},
                new SubCategory{Id = 509, CategoryId = 6, Name = "Socks & Underwear"},
                new SubCategory{Id = 510, CategoryId = 6, Name = "Sunglasses"},
                new SubCategory{Id = 511, CategoryId = 6, Name = "Supreme"},
                new SubCategory{Id = 512, CategoryId = 6, Name = "Ties & Pocketsquares"}
            );
        }
    }
}