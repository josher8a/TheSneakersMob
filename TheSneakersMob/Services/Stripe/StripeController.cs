using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Microsoft.AspNetCore.Http;
using System;
using TheSneakersMob.Models.Common;
using TheSneakersMob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TheSneakersMob.Services.Stripe
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StripeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StripeController(ApplicationDbContext context)
        {
            _context = context;
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