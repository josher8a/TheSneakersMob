using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TheSneakersMob.Infrastructure.Data;

namespace TheSneakersMob.Services.Profiles
{
    public class MustOwnProfileHandler : AuthorizationHandler<MustOwnProfileRequirement>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MustOwnProfileHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnProfileRequirement requirement)
        {
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();
            var success = Int32.TryParse(routeData.Values["id"].ToString(), out int clientIdAsInt);
            if(!success)
                return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var userId = claim != null ? claim.Value : "0";
            var clientToModify = _dbContext.Clients.FirstOrDefault(c => c.Id == clientIdAsInt);
            
            if(clientToModify.UserId != userId)
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class MustOwnProfileRequirement : IAuthorizationRequirement { }

}