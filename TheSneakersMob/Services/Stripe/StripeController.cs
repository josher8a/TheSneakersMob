using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Microsoft.AspNetCore.Http;
using System;
using TheSneakersMob.Models.Common;
using TheSneakersMob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TheSneakersMob.Infrastructure.Stripe;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace TheSneakersMob.Services.Stripe
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StripeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StripeService _stripeService;
        public StripeController(ApplicationDbContext context, StripeService stripeService)
        {
            _stripeService = stripeService;
            _context = context;
        }

        [HttpGet("/connect/oauth")]
        public async Task<IActionResult> HandleOAuthRedirect([FromQuery] string state, [FromQuery] string code)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value.ToString();
            var client = await _context.Clients.FirstOrDefaultAsync(s => s.UserId == userId);

            string connectedAccountId = await _stripeService.SendOuathTokenAsync(code);
            client.StripeId = connectedAccountId;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("webhook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PaymentCompleted()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    string actionType = paymentIntent.Metadata["ActionType"];
                    int actionId = Convert.ToInt32(paymentIntent.Metadata["ActionId"]);
                    int buyerId = Convert.ToInt32(paymentIntent.Metadata["BuyerId"]);

                    var buyer = await _context.Clients.FindAsync(buyerId);

                    bool succcess = Enum.TryParse(actionType, out ActionType actionTypeAsEnum);

                    if (actionTypeAsEnum == ActionType.Sell)
                    {
                        var sell = await _context.Sells
                            .Include(s => s.Seller)
                            .Include(s => s.Buyer)
                                .ThenInclude(b => b.PromoCode)
                            .FirstOrDefaultAsync(s => s.Id == actionId);
                        sell.MarkAsCompleted(buyer);
                    }
                    else if (actionTypeAsEnum == ActionType.Auction)
                    {
                        var auction = await _context.Auctions
                            .Include(a => a.Auctioner)
                            .Include(a => a.Buyer)
                            .FirstOrDefaultAsync(a => a.Id == actionId);
                        auction.MarkAsCompleted(buyer);
                    }
                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

    }

}