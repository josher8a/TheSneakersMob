using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Models;

namespace TheSneakersMob.Services.Auctions
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuctionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AuctionController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
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

            auction.EditBasicInfo(designers,size,dto.Color,dto.Condition,
                dto.Description,photos,hashTags);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete ("{id}")]
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

        [HttpPost ("{id}")]
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

            var result = auction.DirectBuy(buyer);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost ("{id}")]
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
            
            var bid = new Bid(new Money(dto.Amount, dto.Currency),bidder,DateTime.Now);

            var result = auction.Bid(bid);
            if(result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}