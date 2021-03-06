using System;
using System.Collections.Generic;
using System.Linq;

namespace TheSneakersMob.Models
{
    public class Client
    {
        public int Id { get; set;}
        public string UserId { get; private set;}
        public string UserName { get; private set;}
        public string Email { get; set; }
        public string FirstName { get; private set;}
        public string LastName { get; private set;}
        public string Country { get; private set;}
        public string PhotoUrl { get; set; }
        public string StripeId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Sell> Sells { get; set; }
        public List<Sell> Buys { get; set; }
        public List<Auction> AuctionsCreated { get; set;    }
        public List<Auction> AuctionsWon { get; set; }
        public List<ClientFollower> Followers { get; set; } = new List<ClientFollower>();
        public List<ClientFollower> Following { get; set; } = new List<ClientFollower>();
        public PromoCode PromoCode { get; set; }
        // public List<Sell> SellsParticipated { get; set; }

        private Client() { }
        public Client(string userId, string userName, string email, string firstName, string lastName, string country, PromoCode promoCode)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            RegistrationDate = DateTime.Now;
            PromoCode = promoCode;
        }

        public void AddFollower(Client user)
        {
            if(Followers.Exists(f => f.Follower == user))
                return;

            Followers.Add(new ClientFollower{
                Client = this,
                Follower = user
            });
        }

        public void RemoveFollower(Client user)
        {
            var follower = Followers.Find(f => f.Follower == user);
            if (follower is null)
                return;
            
            Followers.Remove(follower);
        }

        public void EditProfile(string firstName, string lastName, string email, string country, string photoUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
            PhotoUrl = photoUrl;
        }

        public void RemoveSells() => Sells.ForEach(s => s.Remove());
    }
}