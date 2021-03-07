using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSneakersMob.Models
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        public PromoCode(string title, int discountPercentage, DateTime validFrom, DateTime validUntil)
        {
            if (validFrom > validUntil)
            {
                throw new ArgumentOutOfRangeException(nameof(validUntil), "End promotion date cannot be before the start");
            }

            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercentage), "The discount percentage must be between 0 and 100");
            }

            Title = title;
            DiscountPercentage = discountPercentage;
            ValidFrom = validFrom;
            ValidUntil = validUntil;

        }

        public bool IsValid() => DateTime.Now < ValidUntil;

    }
}
