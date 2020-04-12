using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Common;
using TheSneakersMob.Services.Validations;

namespace TheSneakersMob.Services.Auctions
{
    public class AuctionForEditDto
    {
        public List<string> Designers { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [EnsureMinimumElements(1, ErrorMessage = "At least one photo is required")]
        [EnsureMaximumElements(10, ErrorMessage = "No more than 10 photos can be provided")]
        public List<PhotoDto> Photos { get; set; }
        
        [Required]
        [EnsureMinimumElements(3, ErrorMessage = "At least 3 hashtags are required")]
        [EnsureMaximumElements(20, ErrorMessage = "No more than 20 hashtags can be provided")]
        public List<string> HashTags { get; set; }
        
    }
}