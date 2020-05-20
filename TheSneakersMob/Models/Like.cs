using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client User { get; set; }
        public Sell Sell { get; set; }
        public Auction Auction { get; set; }
        private Like()
        {
            
        }

        public Like(Client user)
        {
            Date = DateTime.Now;
            User = user;
        }
    }
}