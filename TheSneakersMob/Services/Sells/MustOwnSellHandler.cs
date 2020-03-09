using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Sells
{
    public class MustOwnSellHandler : AuthorizationHandler<MustOwnSellRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MustOwnSellHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnSellRequirement requirement)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();
            var success = Int32.TryParse(routeData.Values["id"].ToString(), out int sellIdAsInt);
            if(!success)
                return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var userId = claim != null ? claim.Value : "0";
            var seller = _dbContext.Clients.FirstOrDefault(c => c.UserId == userId);
            var sell = _dbContext.Sells.Find(sellIdAsInt);

            if (sell is null || sell.SellerId != seller?.Id)
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class MustOwnSellRequirement : IAuthorizationRequirement { }
}