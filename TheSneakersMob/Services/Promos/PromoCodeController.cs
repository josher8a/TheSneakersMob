using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Models;

namespace TheSneakersMob.Services.Promos
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PromoCodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PromoCodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CodeForCreationDto dto)
        {
            if (_context.PromoCodes.Any(c => c.Title == dto.Title))
                return BadRequest("There is already a code with the same name");

            var code = new PromoCode(dto.Title, dto.DiscountPercentage, dto.ValidFrom, dto.ValidUntil);

            await _context.PromoCodes.AddAsync(code);

            await _context.SaveChangesAsync();

            return StatusCode(201, new { Id = code.Id });
        }
    }
}
