using System;
using System.Collections.Generic;
using System.Linq;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Auction : AuditableEntity
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public DateTime ExpireDate { get; set; }
        public Money DirectBuyPrice { get; set; }
        public Money InitialPrize { get; set; }
        public Client Auctioner { get; set; }
        public List<Bid> Bids { get; set; } = new List<Bid> ();
        public Client Buyer { get; set; }
        public Feedback Feedback { get; set; }
        public List<HashTag> HashTags { get; set; }
        public List<Report> Reports { get; set; }
        public bool Removed { get; set; }
        public List<Like> Likes { get; set; } = new List<Like>();

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

        public Result CanDirectBuy(Client buyer)
        {
            if (DirectBuyPrice is null)
                return Result.Fail("This auction does not have a direct buy option");
            
            if (!(Buyer is null))
                return Result.Fail("This item has been sold already");

            if (buyer == Auctioner)
                return Result.Fail("You cannot buy your own product");

            if (DateTime.Now > ExpireDate)
                return Result.Fail("This auction has already expired");

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

        public void MarkAsCompleted(Client buyer)
        {
            if (CanDirectBuy(buyer).Failure)
                throw new InvalidOperationException("Something went wrong when validating the auction in the final step");
            
            Buyer = buyer;
        }

        public Result AddFeedBack(Feedback feedback, Client user)
        {
            if (!(Feedback is null))
                return Result.Fail("Feedback has already been provided for this item");

            if (user != Buyer)
                return Result.Fail("You cannot leave feedback on a product you havent bought");
            
            Feedback = feedback;
            return Result.Success();
        }

        public Result Report(Report report)
        {
            if(Reports.Find(r => r.Reporter == report.Reporter) != null)
                return Result.Fail("You cannot report the same auction twice");
            
            Reports.Add(report);
            if (ShouldRemove())
                Removed = true;

            return Result.Success();
        }

        public bool ShouldBanUser() => Reports.Count(r => r.Severity == Severity.High) >= 5;

        public Result Like(Client user)
        {
            if (Likes.Any(l => l.User == user))
                return Result.Fail("You already like this auction!");

            Likes.Add(new Like(user));
            return Result.Success();
        }

        public Money CalculateFee()
        {
            var feeAmount = DirectBuyPrice.Amount * 0.08m;
            return new Money(feeAmount, DirectBuyPrice.Currency);
        }

        private bool ShouldRemove() => Reports.Count(r => r.Severity == Severity.Low) >= 10 
            || Reports.Count(r => r.Severity == Severity.High) >= 5;
    }

}