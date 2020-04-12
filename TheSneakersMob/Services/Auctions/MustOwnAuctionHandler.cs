using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Auctions
{
    public class MustOwnAuctionHandler : AuthorizationHandler<MustOwnAuctionRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MustOwnAuctionHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnAuctionRequirement requirement)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();
            var success = Int32.TryParse(routeData.Values["id"].ToString(), out int auctionIdAsInt);
            if(!success)
                return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var userId = claim != null ? claim.Value : "0";
            var auctioner = _dbContext.Clients.FirstOrDefault(c => c.UserId == userId);
            var auction = _dbContext.Auctions.Find(auctionIdAsInt);

            if (auction is null || auction.Auctioner != auctioner)
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class MustOwnAuctionRequirement : IAuthorizationRequirement { }
}