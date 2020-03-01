using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Sell Sell { get; set; }
        public int SellId { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Style Style { get; set; }
        public Brand Brand { get; set; }
        public List<Designer> Designers { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
    }

    public class Designer : ValueObject
    {
        public string Title { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
        }
    }

    public enum Style 
    {
        Vintage,
        Luxury,
        Y2K,
        Streetwear
    }
    public enum Condition
    {
        New,
        SemiNew,
        OftenUsed,
        Used,
        VeryUsed
    }
}