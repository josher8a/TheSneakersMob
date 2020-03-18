using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public DateTime ExpireDate { get; set; }
        public Money DirectBuyPrice { get; set; }
        public Money InitialPrize { get; set; }
        public Client Auctioner { get; set; }
        public List<Bid> Bids { get; set; } = new List<Bid> ();
        public Client Buyer { get; set; }
        public List<HashTag> HashTags { get; set; }

        private Auction() { }

        public Auction(Client auctioner, Product product, 
            Money initialPrize, List<HashTag> hashTags, DateTime expireDate)
        {
            Auctioner = auctioner;
            Product = product;
            InitialPrize = initialPrize;
            HashTags = hashTags;
            ExpireDate = expireDate;
        }

        public static Result<Auction> NewAuctionWithDirectBuy(Client auctioner, Product product, 
            Money initialPrize, List<HashTag> hashTags,DateTime expireDate,Money directBuyPrice)
        {
            if (directBuyPrice.Currency != initialPrize.Currency)
                throw new InvalidOperationException(nameof(directBuyPrice));

            if (directBuyPrice.Amount < initialPrize.Amount)
                throw new InvalidOperationException(nameof(directBuyPrice));

            var auction = new Auction(auctioner, product, initialPrize,
                hashTags, expireDate)
            {
                DirectBuyPrice = directBuyPrice
            };

            return Result.Success(auction);
        }
    }

}