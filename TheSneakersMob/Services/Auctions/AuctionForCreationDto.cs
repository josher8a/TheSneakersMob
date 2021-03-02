using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheSneakersMob.Models;
using TheSneakersMob.Models.Common;
using TheSneakersMob.Services.Common;
using TheSneakersMob.Services.Validations;

namespace TheSneakersMob.Services.Auctions
{
    public class AuctionForCreationDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [Required]
        public Style Style { get; set; }

        [Required]
        public int BrandId { get; set; }

        public List<string> Designers { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public Condition Condition { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 9999999999999999.99)]
        public decimal InitialPrize { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public bool IsDirectBuyAllowed { get; set; }

        public decimal? DirectBuyPrize { get; set; }

        [Required]
        //[RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$",ErrorMessage="Please enter a valid date formate")]
        public string ExpireDate { get; set; }

        [Required]
        [EnsureMinimumElements(1, ErrorMessage = "At least one photo is required")]
        [EnsureMaximumElements(10, ErrorMessage = "No more than 10 photos can be provided")]
        public List<PhotoDto> Photos { get; set; }
        
        [Required]
        [EnsureMinimumElements(3, ErrorMessage = "At least 3 hashtags are required")]
        [EnsureMaximumElements(20, ErrorMessage = "No more than 20 hashtags can be provided")]
        public List<string> HashTags { get; set; }

        [Required]
        public Gender Gender { get; set; }

    }
}