using System.ComponentModel.DataAnnotations;
using TheSneakersMob.Models;

namespace TheSneakersMob.Services.Auctions
{
    public class BidDto
    {
        [Range(1, 9999999999999999.99)]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}