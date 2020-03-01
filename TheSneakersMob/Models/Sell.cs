using System.Collections.Generic;

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

    }
}