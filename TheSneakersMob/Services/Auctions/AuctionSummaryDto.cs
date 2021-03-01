using System.Collections.Generic;

namespace TheSneakersMob.Services.Auctions
{
    public class AuctionSummaryDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }
        public string Size { get; set; }
        public string InitialPrize { get; set; }
        public string DirectBuyPrize { get; set; }
        public List<BidSummaryDto> Bids{ get; set; }
        public string CurrentPrize { get; set; }
        public string MainPictureUrl { get; set; }

    }
}