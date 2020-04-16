using System.Collections.Generic;

namespace TheSneakersMob.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> ValidSubCategories { get; set; }  = new List<SubCategory>();
        public List<Size> ValidSizes { get; set; } = new List<Size>();

        public bool IsSubCategoryValid(SubCategory subcategory) => ValidSubCategories.Contains(subcategory);
        public bool IsSizeValid(Size size) => ValidSizes.Contains(size);
        
    }

    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}