using System.Collections.Generic;

namespace TheSneakersMob.Models
{
    public class Client
    {
        public int Id { get; set;}
        public string UserId { get; private set;}
        public string UserName { get; private set;}
        public string FirstName { get; private set;}
        public string LastName { get; private set;}
        public string Country { get; private set;}
        public List<Sell> Sells { get; set; }
        public List<Sell> Buys { get; set; }
        // public List<Sell> SellsParticipated { get; set; }

        public Client(string userId, string userName, string firstName, string lastName, string country)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
        }
    }
}