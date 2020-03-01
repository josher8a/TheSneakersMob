using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Money :ValueObject
    {      
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Increase(decimal amount)
        {
            return new Money(Amount + amount,Currency);
        }

        public static Money Zero(Currency currency)
        {
            return new Money(0,currency);
        }

        public override string ToString()
        {
            var amount = Amount.ToString("0.00");

            return Currency switch
            {
                Currency.Dollar => "$" + amount,
                Currency.Euro => "€" + amount,
                _ => "",
            };
        }

        

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
    public enum Currency
    {
        Dollar,
        Euro
    }
}