using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Infrastructure.Stripe;
using TheSneakersMob.Models;
using TheSneakersMob.Models.Common;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Auctions
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuctionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StripeService _stripeService;
        private readonly AuctionRepository _auctionRepository;

        public AuctionController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, StripeService stripeService, AuctionRepository auctionRepository)
        {
            _stripeService = stripeService;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Auction/Create
        ///     {
        ///         "title": "First Auction",
        ///         "categoryId": 1,
        ///         "subCategoryId": 1,
        ///         "style": "Vintage",
        ///         "brandId": 1,
        ///         "sizeId": 1,
        ///         "color": "blue",
        ///         "condition": "New",
        ///         "description": "First Auction Description",
        ///         "initialPrize": 10,
        ///         "currency": "Dollar",
        ///         "isDirectBuyAllowed": true,
        ///         "directBuyPrize": 20,
        ///         "expireDate": "04/20/2020",
        ///         "photos": [
        ///           {
        ///             "title": "My photo",
        ///             "url": "http://myphoto.com"
        ///           }
        ///         ],
        ///         "hashTags": [
        ///           "awesome","auction","BidNow"
        ///         ]
        ///     }        
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(AuctionForCreationDto dto)
        {
            var category = await _context.Categories
                .Include(c => c.ValidSubCategories)
                .FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
            if (category is null)
                return BadRequest("The selected category does not exist");

            var subCategory = await _context.SubCategories.FindAsync(dto.SubCategoryId);
            if (subCategory is null)
                return BadRequest("The selected sub category does not exist");
            if (!category.IsSubCategoryValid(subCategory))
                return BadRequest("The selected sub category is invalid for this category.");

            var size = await _context.Sizes.FindAsync(dto.SizeId);
            if (size is null)
                return BadRequest("The selected size does not exist");
            if (!category.IsSizeValid(size))
                return BadRequest("The selected size is invalid for this category.");

            var brand = await _context.Brands.FindAsync(dto.BrandId);
            if (brand is null)
                return BadRequest("The selected brand does not exist");

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var auctioner = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (auctioner is null)
                return BadRequest("No user registered with the given id");

            var photos = _mapper.Map<List<Photo>>(dto.Photos);
            var hashTags = dto.HashTags.Select(title => new HashTag(title)).ToList();
            var initialPrize = new Money(dto.InitialPrize, dto.Currency);

            var product = new Product(dto.Title, category, subCategory, dto.Style, brand,
                size, dto.Color, dto.Condition, dto.Description, photos);

            Auction auction;
            if (dto.IsDirectBuyAllowed)
            {
                var directBuyPrize = new Money(dto.DirectBuyPrize.Value, dto.Currency);
                var result = Auction.NewAuctionWithDirectBuy(auctioner, product, initialPrize,
                    hashTags, DateTime.Parse(dto.ExpireDate), directBuyPrize);
                if (result.Failure)
                    return BadRequest(result.Error);
                auction = result.Value;
            }
            else
                auction = new Auction(auctioner, product, initialPrize, hashTags, DateTime.Parse(dto.ExpireDate));

            await _context.AddAsync(auction);
            await _context.SaveChangesAsync();

            return StatusCode(201, auction.Id);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Auction/Edit/id
        ///     {
        ///       "designers": [
        ///         "designer1", "designer2"
        ///       ],
        ///       "sizeId": 2,
        ///       "color": "white",
        ///       "condition": "SemiNew",
        ///       "description": "This auction has been edited",
        ///       "photos": [
        ///         {
        ///           "title": "MyPhoto",
        ///           "url": "https://editedphoto.com"
        ///         }
        ///       ],
        ///       "hashTags": [
        ///         "editing","is","awesome"
        ///       ]
        ///     }   
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize("MustOwnAuction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Edit(int id, AuctionForEditDto dto)
        {
            var auction = await _context.Auctions
               .Include(c => c.Product).ThenInclude(c => c.Category)
               .FirstOrDefaultAsync(c => c.Id == id);
            if (auction is null)
                return NotFound("The sell you are trying to edit does not exists");

            var size = await _context.Sizes.FindAsync(dto.SizeId);
            if (size is null)
                return BadRequest("The selected size does not exist");
            if (!auction.Product.Category.IsSizeValid(size))
                return BadRequest("The selected size is invalid for this category.");

            var photos = _mapper.Map<List<Photo>>(dto.Photos);
            var designers = dto.Designers.Select(title => new Designer(title)).ToList();
            var hashTags = dto.HashTags.Select(title => new HashTag(title)).ToList();

            auction.EditBasicInfo(designers, size, dto.Color, dto.Condition,
                dto.Description, photos, hashTags);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize("MustOwnAuction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction is null)
                return NotFound("The auction you are trying to delete does not exists");

            _context.Remove(auction);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResponse<AuctionSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuctions([FromQuery] GetAuctionsParameters parameters)
        {
            var response = await _auctionRepository.GetAuctionsAsync(parameters);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuctionDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Bids).ThenInclude(b => b.Bidder)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (auction is null || auction.Removed == true)
                return NotFound("No auction found with the id provided");

            var lastBidAmount = auction.Bids.LastOrDefault()?.Amount.ToString();
            var lastBidUserName = auction.Bids.LastOrDefault()?.Bidder.UserName;

            var auctionDto = await _context.Auctions.AsNoTracking()
                .Select(a => new AuctionDetailDto
                {
                    Id = a.Id,
                    Title = a.Product.Title,
                    Category = a.Product.Category.Name,
                    Brand = a.Product.Brand.Name,
                    Designers = a.Product.Designers.Select(d => d.Title).ToList(),
                    Size = a.Product.Size.Description,
                    Color = a.Product.Color,
                    Condition = a.Product.Condition.ToString(),
                    Description = a.Product.Description,
                    InitialPrize = a.InitialPrize.ToString(),
                    DirectBuyPrize = a.DirectBuyPrice.ToString(),
                    LastBidAmount = lastBidAmount,
                    LastBidUserName = lastBidUserName,
                    ExpirationDate = a.ExpireDate.ToString(),
                    Photos = a.Product.Photos.Select(p => new PhotoDto
                    {
                        Title = p.Title,
                        Url = p.Url
                    }).ToList(),
                    HashTags = a.HashTags.Select(h => h.Title).ToList(),
                    UserName = a.Auctioner.UserName,
                    UserCountry = a.Auctioner.Country,
                    NumberOfSells = a.Auctioner.Sells.Count(),
                    UserGeneralFeedback = (decimal)(a.Auctioner.Sells.Where(s => s.Feedback != null).Sum(s => s.Feedback.Stars) + a.Auctioner.AuctionsCreated.Where(s => s.Feedback != null).Sum(a => a.Feedback.Stars))
                        / (a.Auctioner.Sells.Count(s => s.Feedback != null) + a.Auctioner.AuctionsCreated.Count(a => a.Feedback != null)),
                    UserProfilePhoto = a.Auctioner.PhotoUrl,
                    Likes = a.Likes.Count()
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(auctionDto);
        }


        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DirectBuy(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.Buyer)
                .Include(a => a.Auctioner)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (auction is null)
                return NotFound();

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var buyer = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (buyer is null)
                return BadRequest("No user registered with the given id");

            var result = auction.CanDirectBuy(buyer);
            if (result.Failure)
                return BadRequest(result.Error);

            var fee = auction.CalculateFee();

            var clientSecret = await _stripeService
                .CreatePaymentIntentAsync(auction.DirectBuyPrice, fee, ActionType.Auction, auction.Id, buyer.Id, auction.Auctioner.StripeId);

            return Ok(clientSecret);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Bid(int id, BidDto dto)
        {
            var auction = await _context.Auctions
               .Include(a => a.Buyer)
               .Include(a => a.Auctioner)
               .Include(a => a.Bids)
               .FirstOrDefaultAsync(a => a.Id == id);
            if (auction is null)
                return NotFound();

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var bidder = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (bidder is null)
                return BadRequest("No user registered with the given id");

            var bid = new Bid(new Money(dto.Amount, dto.Currency), bidder, DateTime.Now);

            var result = auction.Bid(bid);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Feedback(int id, PostFeedbackDto dto)
        {
            var auction = await _context.Auctions
                .Include(a => a.Buyer)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (auction is null)
                return NotFound("No auction found matching the given id");
            if (auction.Buyer is null)
                return BadRequest("This item has not yet been sold. Feedback is only available to items already sold");

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var user = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (user is null)
                return BadRequest("No user registered with the given id");

            var feedback = new Feedback(dto.Stars, dto.Comment);
            var result = auction.AddFeedBack(feedback, user);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Report(int id, ReportAuctionDto dto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var reporter = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (reporter is null)
                return BadRequest("No user registered with the given id");

            var auction = await _context.Auctions
                .Include(a => a.Auctioner)
                    .ThenInclude(auctioner => auctioner.AuctionsCreated)
                    .ThenInclude(auction => auction.Buyer)
                .Include(a => a.Reports).ThenInclude(r => r.Reporter)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction is null)
                return NotFound("No auction found matching the given id");

            var report = Models.Report.Create(dto.Reason, reporter);
            var result = auction.Report(report);
            if (result.Failure)
                return BadRequest(result.Error);

            if (auction.ShouldBanUser())
            {
                auction.Auctioner.RemoveSells();
                var userToBan = await _userManager.FindByIdAsync(auction.Auctioner.UserId);
                userToBan.BannedUntil = DateTime.Now.AddDays(Models.Report.BannedDays);
                await _userManager.UpdateAsync(userToBan);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Like(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var user = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (user is null)
                return BadRequest("No user registered with the given id");

            var auction = await _context.Auctions
                .Include(a => a.Likes).ThenInclude(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (auction is null || auction.Removed)
                return NotFound("No sell found with the given id");

            var result = auction.Like(user);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}