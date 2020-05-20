using System;
using System.Collections.Generic;
using System.Linq;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Sell
    {
        public int Id { get; set; }
        public Client Seller { get; set; }
        public int SellerId { get; set; }
        public Client Buyer { get; set; }
        public int? BuyerId{ get; set; }
        public Product Product { get; set; }
        public Money Price { get; set; }
        public Feedback Feedback { get; set; }
        public List<HashTag> HashTags { get; set; }
        public List<Report> Reports { get; set; } = new List<Report>();
        public bool Removed { get; set; }
        public List<Like> Likes { get; set; } = new List<Like>();

        // //List of places and terms where a product can be shipped
        // public List<Shipping> ShippingAvailables { get; set; 

        private Sell()
        {
            
        }
        public Sell(Client seller, Product product, Money price, List<HashTag> hashTags)
        {
            Seller = seller;
            Product = product;
            Price = price;
            HashTags = hashTags;
            Removed = false;
        }

        public void EditBasicInfo(string title, Category category, SubCategory subCategory, Style style, 
            Brand brand, List<Designer> designers, Size size, string color, Condition condition, string description, 
            Money price, List<Photo> photos, List<HashTag> hashTags)
        {
            if (price.Currency != Price.Currency)
                throw new InvalidOperationException(nameof(Price));
            Price = price;
            HashTags = hashTags;
            Product.EditBasicInfoForSell(title, category, subCategory, style, brand, designers, size, 
                color, condition,description, photos);                     
        }

        public Result MarkAsSold(Client buyer)
        {
            if (!(Buyer is null))
                return Result.Fail("This item has been sold already");

            if (buyer == Seller)
                return Result.Fail("You cannot buy your own product");

            Buyer = buyer;
            return Result.Success();
        }

        public Result AddFeedback(Feedback feedback, Client user)
        {
            if (!(Feedback is null))
                return Result.Fail("Feedback has already been provided for this item");

            if (user != Buyer)
                return Result.Fail("You cannot leave feedback on a product you havent bought");
            
            Feedback = feedback;
            return Result.Success();
        }

        public void Remove()
        {
            if(Buyer is null)
                Removed = true;
        }

        public Result Report(Report report)
        {
            if (Reports.Any(r => r.Reporter == report.Reporter))
                return Result.Fail("You cannot report the same sell twice");
            
            Reports.Add(report);
            if (ShouldRemove())
                Removed = true;

            return Result.Success();
        }

        private bool ShouldRemove() => Reports.Count(r => r.Severity == Severity.Low) >= 10 
            || Reports.Count(r => r.Severity == Severity.High) >= 5;

        public bool ShouldBanUser() => Reports.Count(r => r.Severity == Severity.High) >= 5;

        public Result Like(Client user)
        {
            if (Likes.Any(l => l.User == user))
                return Result.Fail("You already like this sell!");

            Likes.Add(new Like(user));
            return Result.Success();
        }
    }
}