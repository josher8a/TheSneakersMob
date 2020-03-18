using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Auctions
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuctionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuctionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(AuctionForCreationDto dto)
        {
            return StatusCode(201,auction.Id);
        }



    }
}