using System.ComponentModel.DataAnnotations;

namespace TheSneakersMob.Services.Profiles
{
    public class EditProfileDto
    {
        public string PhotoUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]      
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Country { get; set; }
    }
}