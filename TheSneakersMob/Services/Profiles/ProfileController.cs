using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheSneakersMob.Infrastructure.Data;
using TheSneakersMob.Models;

namespace TheSneakersMob.Services.Profiles
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _context.Clients.AsNoTracking()
                .Select(c => new GetProfileDto
                {
                    Id = c.Id,
                    RegistrationDate = c.RegistrationDate.ToString(),
                    PhotoUrl = c.PhotoUrl,
                    AvarageReview = (decimal)(c.Sells.Where(s => s.Feedback != null).Sum(s => s.Feedback.Stars) + c.AuctionsCreated.Where(s => s.Feedback != null).Sum(a => a.Feedback.Stars)) 
                        / (c.Sells.Count(s => s.Feedback != null) + c.AuctionsCreated.Count(a => a.Feedback != null)),
                    UserName = c.UserName,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Country = c.Country,
                    AmountOfSells = c.Sells.Count() + c.AuctionsCreated.Count(),
                    AmountOfBuys = c.Buys.Count() + c.AuctionsWon.Count(),
                    Followers = c.Followers.Select(f => new GetFollowerDto
                    {
                        Id = f.Follower.Id,
                        UserName = f.Follower.UserName
                    }).ToList()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profile is null)
                return NotFound("The requested profile does not exist");

            return Ok(profile);
        }

        [HttpPut ("{id}")]
        [Authorize("MustOwnProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, EditProfileDto dto)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(p => p.Id == id);
            if (client is null)
                return NotFound("The requested profile does not exists");

            var user = await _userManager.FindByIdAsync(client.UserId);

            client.EditProfile(dto.Name, dto.LastName, dto.Email, dto.Country, dto.PhotoUrl);
            user.EditProfileInfo(dto.Name, dto.LastName, dto.Email, dto.Country);
            await _userManager.UpdateAsync(user);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Follow(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var currentUser = await _context.Clients
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (currentUser is null)
                return NotFound("The current user does not exists");

            var userToFollow = await _context.Clients
                .Include(c => c.Followers)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (userToFollow is null)
                return NotFound("The requested user does not exists");

            if (currentUser == userToFollow)
                return BadRequest("You cannot follow yourself");

            userToFollow.AddFollower(currentUser);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnFollow(int id)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var currentUser = await _context.Clients
                .Include(u => u.Following)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (currentUser is null)
                return NotFound("The current user does not exists");

            var targetUser = await _context.Clients
                .Include(c => c.Followers)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (targetUser is null)
                return NotFound("The requested user does not exists");

            targetUser.RemoveFollower(currentUser);

            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}