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

namespace TheSneakersMob.Services.Sells
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SellController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SellController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

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

            var brand = await _context.Brands.FindAsync(dto.BrandId);
            if (brand is null)
                return BadRequest("The selected brand does not exist");

            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var seller = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);
            if (seller is null)
                return BadRequest("No user registered with the given id");

            var photos = _mapper.Map<List<Photo>>(dto.Photos);

            var product = new Product(dto.Title, category, subCategory, dto.Style, brand, 
                dto.Size, dto.Color, dto.Condition, dto.Description,photos);
            var sell = new Sell(seller,product,dto.Amount,dto.Currency);

            await _context.AddAsync(sell);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete ("{id}")]
        [Authorize("MustOwnSell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sell = await _context.Sells.FindAsync(id);
            if (sell is null)
                return BadRequest("The sell you are trying to delete does not exists");

            _context.Remove(sell);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}