using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Auctions
{
    public class GetAuctionsParameters : QueryStringParameters
    {
        public string Category { get; set; }
        public string Subcategoy { get; set; }

        public string Style { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Condition { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string SearchQuery { get; set; }
    }
}
