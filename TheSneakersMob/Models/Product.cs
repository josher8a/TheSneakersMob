using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public Style Style { get; set; }
        public Brand Brand { get; set; }
        public List<Designer> Designers { get; set; }
        public Size Size { get; set; }
        public string Color { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }
        public Gender Gender { get; set; }

        private Product()
        {          
        
        }
        
        public Product(string title, Category category, SubCategory subCategory, Style style, Brand brand, 
            Size size, string color, Condition condition, string description, List<Photo> photos, Gender gender)
        {
            Title = title;
            Category = category;
            SubCategory = Category.IsSubCategoryValid(subCategory) ?  subCategory : throw new Exception(nameof(subCategory));
            Style = style;
            Brand = brand;
            Size = Category.IsSizeValid(size) ? size : throw new Exception(nameof(size));
            Color = color;
            Condition = condition;
            Description = description;
            Photos = photos;
            Gender = gender;
        }

        public void EditBasicInfoForSell(string title, Category category, SubCategory subCategory, Style style, Brand brand, List<Designer> designers, Size size, 
            string color, Condition condition, string description, List<Photo> photos)
        {
            Title = title;
            Category = category;
            SubCategory = Category.IsSubCategoryValid(subCategory) ?  subCategory : throw new Exception(nameof(subCategory));
            Style = style;
            Brand = brand;
            Size = Category.IsSizeValid(size) ? size : throw new Exception(nameof(size));
            Color = color;
            Condition = condition;
            Description = description;
            Photos = photos;
            Designers = designers;
        }

        public void EditBasicInfoForAuction(List<Designer> designers, Size size, string color, Condition condition, string description, List<Photo> photos)
        {
            Size = Category.IsSizeValid(size) ? size : throw new Exception(nameof(size));
            Color = color;
            Condition = condition;
            Description = description;
            Photos = photos;
            Designers = designers;
        }
    }

    public class Designer : ValueObject
    {
        public string Title { get; set; }

        public Designer(string title)
        {
            Title = title;
        }
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