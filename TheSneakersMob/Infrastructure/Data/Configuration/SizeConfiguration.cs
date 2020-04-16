using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSneakersMob.Models;

namespace TheSneakersMob.Infrastructure.Data.Configuration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasData(
                //Tops
                new Size{Id = 1, CategoryId = 1, Description = "US XXS/EU 40"},
                new Size{Id = 2, CategoryId = 1, Description = "US XS/EU 42/0"},
                new Size{Id = 3, CategoryId = 1, Description = "US S/ EU 44-46/1"},
                new Size{Id = 4, CategoryId = 1, Description = "US M/ EU 48-50/2"},
                new Size{Id = 5, CategoryId = 1, Description = "US L/EU 52-54/3"},
                new Size{Id = 6, CategoryId = 1, Description = "US XL/EU 56/4"},
                new Size{Id = 7, CategoryId = 1, Description = "US S/EU 58/5"},

                //Bottoms
                new Size{Id = 100, CategoryId = 2, Description = "US 26/EU 42"},
                new Size{Id = 101, CategoryId = 2, Description = "US 27"},
                new Size{Id = 102, CategoryId = 2, Description = "US28/EU 44"},
                new Size{Id = 103, CategoryId = 2, Description = "US 29"},
                new Size{Id = 104, CategoryId = 2, Description = "US 30/EU 44"},
                new Size{Id = 105, CategoryId = 2, Description = "US 31"},
                new Size{Id = 106, CategoryId = 2, Description = "US 32/EU 48"},
                new Size{Id = 107, CategoryId = 2, Description = "US 33"},
                new Size{Id = 108, CategoryId = 2, Description = "US 34/EU 50"},
                new Size{Id = 109, CategoryId = 2, Description = "US 35"},
                new Size{Id = 110, CategoryId = 2, Description = "US 36/ EU 52"},
                new Size{Id = 111, CategoryId = 2, Description = "US 37"},
                new Size{Id = 112, CategoryId = 2, Description = "US 38/EU 54"},
                new Size{Id = 113, CategoryId = 2, Description = "US 39"},
                new Size{Id = 114, CategoryId = 2, Description = "US 40/EU 56"},
                new Size{Id = 115, CategoryId = 2, Description = "US 42/EU 58"},
                new Size{Id = 116, CategoryId = 2, Description = "US 43"},
                new Size{Id = 117, CategoryId = 2, Description = "US 44/EU 60"},

                //Outwear
                new Size{Id = 201, CategoryId = 3, Description = "US XXS/EU 40"},
                new Size{Id = 202, CategoryId = 3, Description = "US XS/EU 42/0"},
                new Size{Id = 203, CategoryId = 3, Description = "US S/ EU 44-46/1"},
                new Size{Id = 204, CategoryId = 3, Description = "US M/ EU 48-50/2"},
                new Size{Id = 205, CategoryId = 3, Description = "US L/EU 52-54/3"},
                new Size{Id = 206, CategoryId = 3, Description = "US XL/EU 56/4"},
                new Size{Id = 207, CategoryId = 3, Description = "US S/EU 58/5"},
                
                //Boots
                new Size{Id = 301, CategoryId = 4, Description = "US 5/EU 37 "},
                new Size{Id = 302, CategoryId = 4, Description = "US 5.5/EU 38"},
                new Size{Id = 303, CategoryId = 4, Description = "US 6/EU 39"},
                new Size{Id = 304, CategoryId = 4, Description = "US 6.5/EU 39-40"},
                new Size{Id = 305, CategoryId = 4, Description = "US 7/EU 40"},
                new Size{Id = 306, CategoryId = 4, Description = "US 7.5/EU 40-41"},
                new Size{Id = 307, CategoryId = 4, Description = "US 8/EU 41"},
                new Size{Id = 308, CategoryId = 4, Description = "US 8.5/ EU 41-42"},
                new Size{Id = 309, CategoryId = 4, Description = "US 9/EU 42"},
                new Size{Id = 310, CategoryId = 4, Description = "US 9.5/EU 42-43"},
                new Size{Id = 311, CategoryId = 4, Description = "US 10/EU 43"},
                new Size{Id = 312, CategoryId = 4, Description = "US 10.5/EU 43-44"},
                new Size{Id = 313, CategoryId = 4, Description = "US 11/EU 44"},
                new Size{Id = 314, CategoryId = 4, Description = "US 11.5/EU 44-55"},
                new Size{Id = 315, CategoryId = 4, Description = "US 12/EU 55"},
                new Size{Id = 316, CategoryId = 4, Description = "US 12.5/EU 55-56"},
                new Size{Id = 317, CategoryId = 4, Description = "US 13/EU 56"},
                new Size{Id = 318, CategoryId = 4, Description = "US 14/EU 57"},
                new Size{Id = 319, CategoryId = 4, Description = "US 15/EU 58"},

                //Tailoring
                new Size{Id = 401, CategoryId = 5, Description = "34S"},
                new Size{Id = 402, CategoryId = 5, Description = "34R"},
                new Size{Id = 403, CategoryId = 5, Description = "36S"},
                new Size{Id = 404, CategoryId = 5, Description = "36R"},
                new Size{Id = 405, CategoryId = 5, Description = "38S"},
                new Size{Id = 406, CategoryId = 5, Description = "38R"},
                new Size{Id = 407, CategoryId = 5, Description = "38L"},
                new Size{Id = 408, CategoryId = 5, Description = "40S"},
                new Size{Id = 409, CategoryId = 5, Description = "40R"},
                new Size{Id = 411, CategoryId = 5, Description = "40R"},
                new Size{Id = 412, CategoryId = 5, Description = "40R"},
                new Size{Id = 413, CategoryId = 5, Description = "42S"},
                new Size{Id = 414, CategoryId = 5, Description = "42R"},
                new Size{Id = 415, CategoryId = 5, Description = "44L"},
                new Size{Id = 416, CategoryId = 5, Description = "44S"},
                new Size{Id = 417, CategoryId = 5, Description = "44R"},
                new Size{Id = 418, CategoryId = 5, Description = "44L"},
                new Size{Id = 419, CategoryId = 5, Description = "46S"},
                new Size{Id = 420, CategoryId = 5, Description = "46R"},
                new Size{Id = 421, CategoryId = 5, Description = "46L"},
                new Size{Id = 422, CategoryId = 5, Description = "48S"},
                new Size{Id = 423, CategoryId = 5, Description = "48R"},
                new Size{Id = 424, CategoryId = 5, Description = "48L"},
                new Size{Id = 425, CategoryId = 5, Description = "50S"},
                new Size{Id = 426, CategoryId = 5, Description = "50R"},
                new Size{Id = 427, CategoryId = 5, Description = "50L"},
                new Size{Id = 428, CategoryId = 5, Description = "52S"},
                new Size{Id = 429, CategoryId = 5, Description = "52R"},
                new Size{Id = 430, CategoryId = 5, Description = "52L"},
                new Size{Id = 431, CategoryId = 5, Description = "54S"},
                new Size{Id = 432, CategoryId = 5, Description = "54R"},
                new Size{Id = 433, CategoryId = 5, Description = "54L"},

                //Accesories
                new Size{Id = 501, CategoryId = 6, Description = "One Size"},
                new Size{Id = 502, CategoryId = 6, Description = "26"},
                new Size{Id = 503, CategoryId = 6, Description = "28"},
                new Size{Id = 504, CategoryId = 6, Description = "30"},
                new Size{Id = 505, CategoryId = 6, Description = "32"},
                new Size{Id = 506, CategoryId = 6, Description = "34"},
                new Size{Id = 507, CategoryId = 6, Description = "36"},
                new Size{Id = 508, CategoryId = 6, Description = "38"},
                new Size{Id = 509, CategoryId = 6, Description = "40"},
                new Size{Id = 510, CategoryId = 6, Description = "42"},
                new Size{Id = 511, CategoryId = 6, Description = "44"},
                new Size{Id = 512, CategoryId = 6, Description = "46"}
            );
        }
    }
}