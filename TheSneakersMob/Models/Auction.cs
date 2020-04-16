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
                return Result.Fail<Auction>("The direct buy price must be greater than the initial prize");

            var auction = new Auction(auctioner, product, initialPrize,
                hashTags, expireDate)
            {
                DirectBuyPrice = directBuyPrice
            };

            return Result.Success(auction);
        }

        public void EditBasicInfo(List<Designer> designers, Size size, string color, Condition condition, 
            string description, List<Photo> photos, List<HashTag> hashTags)
        {
            HashTags = hashTags;
            Product.EditBasicInfoForAuction(designers, size, color, condition,description, photos);
        }

        public Result DirectBuy(Client buyer)
        {
            if (DirectBuyPrice is null)
                return Result.Fail("This auction does not have a direct buy option");
            
            if (!(Buyer is null))
                return Result.Fail("This item has been sold already");

            if (buyer == Auctioner)
                return Result.Fail("You cannot buy your own product");

            if (DateTime.Now > ExpireDate)
                return Result.Fail("This auction has already expired");

            Buyer = buyer;
            return Result.Success();
        }

        public Result Bid(Bid bid)
        {
            if (DateTime.Now > ExpireDate)
                return Result.Fail("This auction has already expired");

            if (!(Buyer is null))
                return Result.Fail("This item has been sold already");

            if (bid.Bidder == Auctioner)
                return Result.Fail("You cannot bid for your own product");

            if (InitialPrize.Currency != bid.Amount.Currency)
                return Result.Fail("Currencies dont match");

            if (bid.Amount.Amount < InitialPrize.Amount)
                return Result.Fail("The bid must at least the same as the initial bid price");

            //Todo: Validation for highest bid maybe??

            Bids.Add(bid);
            return Result.Success();
        }
    }

}