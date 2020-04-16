using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Categories
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryDetailDto),StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.AsNoTracking()
                .Select(c => new CategoryDetailDto {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = c.ValidSubCategories.Select(sc => new SubcategoryDetailDto {
                        Id = sc.Id,
                        Name =sc.Name
                    }).ToList(),
                    Sizes = c.ValidSizes.Select(size => new SizeDetailDto {
                        Id = size.Id,
                        Description = size.Description
                    }).ToList()
                }).ToListAsync();

            return Ok(categories);
        }
    }
}