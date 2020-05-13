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
using TheSneakersMob.Models;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Services.Sells
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SellController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Sell/Create
        ///     {
        ///         "title": "Great offer",
        ///         "categoryId": 1,
        ///         "subCategoryId": 1,
        ///         "style": "Vintage",
        ///         "brandId": 1,
        ///         "designers": [
        ///             "string"
        ///         ],
        ///         "sizeId": 1,
        ///         "color": "white",
        ///         "condition": "New",
        ///         "description": "Great shoes",
        ///         "amount": 10,
        ///         "currency": "Dollar",
        ///         "photos": [
        ///             {
        ///             "title": "My Photo",
        ///             "url": "photo.com"
        ///             }
        ///         ],
        ///         "hashTags": [
        ///             "#great","sneakers","BuyThem"
        ///         ]
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(SellForCreationDto dto)
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
            var seller = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (seller is null)
                return BadRequest("No user registered with the given id");

            var photos = _mapper.Map<List<Photo>>(dto.Photos);
            var hashTags = dto.HashTags.Select(title => new HashTag(title)).ToList();
            var price = new Money(dto.Amount, dto.Currency);

            var product = new Product(dto.Title, category, subCategory, dto.Style, brand,
                size, dto.Color, dto.Condition, dto.Description, photos);
            var sell = new Sell(seller, product, price, hashTags);

            await _context.AddAsync(sell);
            await _context.SaveChangesAsync();

            return StatusCode(201, sell.Id);
        }

        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Sell/Edit/id
        ///     {
        ///         "title": "Edit Sell",
        ///         "categoryId": 2,
        ///         "subCategoryId": 102,
        ///         "style": "Vintage",
        ///         "brandId": 1,
        ///         "designers": [
        ///             "Collaborator1"
        ///         ],
        ///         "sizeId": 101,
        ///         "color": "Black",
        ///         "condition": "OftenUsed",
        ///         "description": "This sell has been edited",
        ///         "amount": 750,
        ///         "photos": [
        ///             {
        ///             "title": "Photo1",
        ///             "url": "photo1.com"
        ///             }
        ///         ],
        ///         "hashTags": [
        ///             "edit","is","awesome"
        ///         ]
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize("MustOwnSell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Edit(int id, SellForEditDto dto)
        {
            var sell = await _context.Sells
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (sell is null)
                return NotFound("The sell you are trying to edit does not exists");

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

            var photos = _mapper.Map<List<Photo>>(dto.Photos);
            var designers = dto.Designers.Select(title => new Designer(title)).ToList();
            var hashTags = dto.HashTags.Select(title => new HashTag(title)).ToList();
            var price = new Money(dto.Amount, sell.Price.Currency);

            sell.EditBasicInfo(dto.Title, category, subCategory, dto.Style, brand, designers,
                size, dto.Color, dto.Condition, dto.Description, price, photos, hashTags);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize("MustOwnSell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sell = await _context.Sells.FindAsync(id);
            if (sell is null)
                return NotFound("The sell you are trying to delete does not exists");

            _context.Remove(sell);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SellDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var sell = await _context.Sells.AsNoTracking()
                .Where(s => s.Removed == false)
                .Select(s => new SellDetailDto
                {
                    Id = s.Id,
                    Title = s.Product.Title,
                    Category = s.Product.Category.Name,
                    Brand = s.Product.Brand.Name,
                    Designers = s.Product.Designers.Select(d => d.Title).ToList(),
                    Size = s.Product.Size.Description,
                    Color = s.Product.Color,
                    Condition = s.Product.Condition.ToString(),
                    Description = s.Product.Description,
                    Price = s.Price.ToString(),
                    Photos = s.Product.Photos.Select(p => new PhotoDto
                    {
                        Title = p.Title,
                        Url = p.Url
                    }).ToList(),
                    HashTags = s.HashTags.Select(h => h.Title).ToList(),
                    UserName = s.Seller.UserName,
                    UserCountry = s.Seller.Country,
                    NumberOfSells = s.Seller.Sells.Count(),
                    UserGeneralFeedback = (decimal)(s.Seller.Sells.Where(s => s.Feedback != null).Sum(s => s.Feedback.Stars) + s.Seller.AuctionsCreated.Where(s => s.Feedback != null).Sum(a => a.Feedback.Stars))
                        / (s.Seller.Sells.Count(s => s.Feedback != null) + s.Seller.AuctionsCreated.Count(a => a.Feedback != null)),
                    UserProfilePhoto = s.Seller.PhotoUrl
                })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sell is null)
                return NotFound("No sell found with the id provided");

            return Ok(sell);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Buy(int id)
        {
            var sell = await _context.Sells
                .Include(s => s.Buyer)
                .Include(s => s.Seller)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sell is null)
                return NotFound("No sell found matching the given id");

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var buyer = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (buyer is null)
                return BadRequest("No user registered with the given id");

            var result = sell.MarkAsSold(buyer);
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
            var sell = await _context.Sells
                .Include(s => s.Buyer)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (sell is null)
                return NotFound("No sell found matching the given id");
            if (sell.Buyer is null)
                return BadRequest("This item has not yet been sold. Feedback is only available to items already sold");

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var user = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (user is null)
                return BadRequest("No user registered with the given id");

            var feedback = new Feedback(dto.Stars, dto.Comment);
            var result = sell.AddFeedback(feedback, user);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Report(int id, ReportSellDto dto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var reporter = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (reporter is null)
                return BadRequest("No user registered with the given id");

            var sell = await _context.Sells
                .Include(s => s.Seller).ThenInclude(seller => seller.Sells).ThenInclude(sell => sell.Buyer)
                .Include(s => s.Reports).ThenInclude(r => r.Reporter)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sell is null)
                return NotFound("No sell found matching the given id");

            var report = Models.Report.Create(dto.Reason, reporter);
            var result = sell.Report(report);
            if (result.Failure)
                return BadRequest(result.Error);

            if (sell.ShouldBanUser())
            {
                sell.Seller.RemoveSells();
                var userToBan = await _userManager.FindByIdAsync(sell.Seller.UserId);
                userToBan.BannedUntil = DateTime.Now.AddDays(Models.Report.BannedDays);
                await _userManager.UpdateAsync(userToBan);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}