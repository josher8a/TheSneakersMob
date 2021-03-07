using System;

namespace TheSneakersMob.Services.Promos
{
    public class CodeForCreationDto
    {
        public string Title { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}