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
        public List<HashTag> HashTags { get; set; }

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
        }

        public void EditBasicInfo(string title, Category category, SubCategory subCategory, Style style, 
            Brand brand, List<Designer> designers, string size, string color, Condition condition, string description, 
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
    }
}