using System;
using System.Collections.Generic;
using System.Linq;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Sell : AuditableEntity
    {
        public int Id { get; set; }
        public Client Seller { get; set; }
        public int SellerId { get; set; }
        public Client Buyer { get; set; }
        public int? BuyerId { get; set; }
        public Product Product { get; set; }
        public Money Price { get; set; }
        public Feedback Feedback { get; set; }
        public List<HashTag> HashTags { get; set; }
        public List<Report> Reports { get; set; } = new List<Report>();
        public bool Removed { get; set; }
        public List<Like> Likes { get; set; } = new List<Like>();
        public bool AcceptCoupons { get; set; }

        // //List of places and terms where a product can be shipped
        // public List<Shipping> ShippingAvailables { get; set; 

        private Sell()
        {

        }
        public Sell(Client seller, Product product, Money price, List<HashTag> hashTags, bool acceptCoupons)
        {
            Seller = seller;
            Product = product;
            Price = price;
            HashTags = hashTags;
            Removed = false;
            AcceptCoupons = acceptCoupons;
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
                color, condition, description, photos);
        }

        public Result CanBuy(Client buyer, string promoCode)
        {
            if (!(Buyer is null))
                return Result.Fail("This item has been sold already");

            if (buyer == Seller)
                return Result.Fail("You cannot buy your own product");

            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                if (promoCode != buyer.PromoCode.Title)
                    return Result.Fail("You are not allowed to use that coupon");
                if (!buyer.PromoCode.IsValid())
                    return Result.Fail("The coupon is expired");
            }

            return Result.Success();
        }

        public void MarkAsCompleted(Client buyer)
        {
            if (CanBuy(buyer, null).Failure)
                throw new InvalidOperationException("Something went wrong when validating the sell in the final step");
            
            Buyer = buyer;
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
            if (Buyer is null)
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

        public bool ShouldBanUser() => Reports.Count(r => r.Severity == Severity.High) >= 5;

        public Result Like(Client user)
        {
            if (Likes.Any(l => l.User == user))
                return Result.Fail("You already like this sell!");

            Likes.Add(new Like(user));
            return Result.Success();
        }

        public Money CalculateFee(PromoCode coupon)
        {
            var finalPrice = FinalPrice(coupon);
            var feeAmount = finalPrice.Amount * 0.06m;
            return new Money(feeAmount, finalPrice.Currency);
        }

        public Money FinalPrice(PromoCode coupon)
        {
            if (!AcceptCoupons || coupon is null)
                return Price;

            return new Money(Price.Amount - Price.Amount * (coupon.DiscountPercentage / 100), Price.Currency);
        }

        private bool ShouldRemove() => Reports.Count(r => r.Severity == Severity.Low) >= 10
           || Reports.Count(r => r.Severity == Severity.High) >= 5;
    }
}