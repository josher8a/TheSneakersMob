using System.ComponentModel.DataAnnotations;

namespace TheSneakersMob.Services.Common
{
    public class PhotoDto
    {
        [Required]
        public string Title { get; set; }

        [Required]        
        public string Url { get; set; }
    }
}