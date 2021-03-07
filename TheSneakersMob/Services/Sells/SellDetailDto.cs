using System.Collections.Generic;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Sells
{
    public class SellDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public List<string> Designers { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Gender { get; set; }
        public List<PhotoDto> Photos { get; set; }
        public List<string> HashTags { get; set; }
        public string UserName { get; set; }
        public string UserCountry { get; set; }
        public int NumberOfSells { get; set; }
        public decimal UserGeneralFeedback { get; set; }
        public string UserProfilePhoto { get; set; }
        public int Likes { get; set; }
        public bool AcceptCoupons { get; set; }

        // TODO:
        // + Shipping
    }
}