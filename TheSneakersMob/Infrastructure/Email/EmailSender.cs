using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSneakersMob.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = _emailSettings.Value.ApiKey;
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("noreplay@thesneakersmob.com", "The Sneakers Mob Team");
            var to = new EmailAddress(email);
            var htmlContent = $"<strong>{htmlMessage}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlContent);
            _ = await client.SendEmailAsync(msg);
        }
    }
}
