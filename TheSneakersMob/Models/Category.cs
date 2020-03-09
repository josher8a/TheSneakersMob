using System.Collections.Generic;

namespace TheSneakersMob.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> ValidSubCategories { get; set; }  = new List<SubCategory>();

        public bool IsSubCategoryValid(SubCategory subcategory) => ValidSubCategories.Contains(subcategory);
        
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}