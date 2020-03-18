using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Bid : ValueObject
    {
        public Money Amount { get; set; }
        public Client Bidder { get; set; }
        public DateTime Date { get; set; }

        public Bid(Money amount, Client bidder, DateTime date)
        {
            Amount = amount;
            Bidder = bidder;
            Date = date;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Bidder;
            yield return Date;
        }
    }
}