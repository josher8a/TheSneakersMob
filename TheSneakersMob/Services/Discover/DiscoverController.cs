using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Discover
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DiscoverController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private const int defaultRandomItems = 10;

        public DiscoverController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(DiscoverDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int items)
        {
            int itemsToDiscover = items <= 0 ? defaultRandomItems : items;
            
            Random rnd = new Random();
            int numberOfSellsToTake = rnd.Next(0, itemsToDiscover + 1);
            int numberOfAuctionsToTake = itemsToDiscover - numberOfSellsToTake;
            
            var sells = await _context.Sells
                .Include(s => s.Product)
                .Where(s => s.BuyerId == null)
                .OrderBy(s => Guid.NewGuid())
                .Take(numberOfSellsToTake)
                .ToListAsync();

            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Where(a => a.Buyer == null)
                .OrderBy(a => Guid.NewGuid())
                .Take(numberOfAuctionsToTake)
                .ToListAsync();

            var discoverySells = sells.Select(s => new DiscoverDto
            {
                Id = s.Id,
                Type = "Sell",
                MainPictureUrl = s.Product.Photos.Any() ? s.Product.Photos.First().Url : null
            }).ToList();


            var discoveryAuctions = auctions.Select(a => new DiscoverDto
            {
                Id = a.Id,
                Type = "Auction",
                MainPictureUrl = a.Product.Photos.Any() ? a.Product.Photos.First().Url : null
            }).ToList();


            var response = discoverySells
                .Concat(discoveryAuctions)
                .OrderBy(a => Guid.NewGuid())
                .ToList();

            return Ok(response);
        }
    }
}
