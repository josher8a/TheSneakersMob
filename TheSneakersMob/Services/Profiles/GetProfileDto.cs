using System.Collections.Generic;

namespace TheSneakersMob.Services.Profiles
{
    public class GetProfileDto
    {
        public int Id { get; set; }
        public string RegistrationDate { get; set; }
        public string PhotoUrl { get; set; }
        public decimal AvarageReview { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int AmountOfSells { get; set; }
        public int AmountOfBuys { get; set; }
        public List<GetFollowerDto> Followers { get; set; }
    }

    public class GetFollowerDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}