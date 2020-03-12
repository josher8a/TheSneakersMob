using System.Collections.Generic;

namespace TheSneakersMob.Services.Categories
{
    public class CategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubcategoryDetailDto> Subcategories { get; set; }
    }

    public class SubcategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}