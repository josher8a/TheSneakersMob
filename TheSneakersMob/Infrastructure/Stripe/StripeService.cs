using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Stripe;
using TheSneakersMob.Models;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Infrastructure.Stripe
{
    public class StripeService
    {
        private readonly IOptions<StripeSettings> _stripeSettings;
        private readonly StripeClient _client;
        public StripeService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings;
            _client = new StripeClient(_stripeSettings.Value.SecretKey);
        }

        public async Task<string> CreatePaymentIntentAsync(Money amount, Money fee, ActionType actionType, int actionId, int buyerId, string destinationId)
        {
            var service = new PaymentIntentService();

            long amountInCents = decimal.ToInt64(amount.Amount * 100);
            long feeInCents = decimal.ToInt64(fee.Amount * 100);
            Dictionary<string, string> metadata = new Dictionary<string, string>
            {
                { "ActionType", actionType.ToString() },
                { "ActionId", actionId.ToString() },
                { "BuyerId", buyerId.ToString() }
            };

            var createOptions = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Amount = amountInCents,
                Currency = ConvertCurrency(amount.Currency),
                ApplicationFeeAmount = feeInCents,
                Metadata = metadata,
                TransferData = new PaymentIntentTransferDataOptions
                {
                    Destination = destinationId,
                },
            };

            var result = await service.CreateAsync(createOptions);
            return result.ClientSecret;
        }

        public async Task<string> SendOuathTokenAsync(string code)
        {
            var service = new OAuthTokenService(_client);

            var options = new OAuthTokenCreateOptions
            {
                GrantType = "authorization_code",
                Code = code,
            };

            var response = await service.CreateAsync(options);
            var connectedAccountId = response.StripeUserId;
            return connectedAccountId;
        }

        private string ConvertCurrency(Currency currency) =>
            currency switch
            {
                Currency.Dollar => "usd",
                Currency.Euro => "eur",
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(currency))
            };
    }
}