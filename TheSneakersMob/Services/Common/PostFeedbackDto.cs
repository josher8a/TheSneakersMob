using System.ComponentModel.DataAnnotations;

namespace TheSneakersMob.Services.Common
{
    public class PostFeedbackDto
    {
        [Range(0, 5, ErrorMessage="The feedback must be between 0 and 5 stars")]
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}