using rgomezj.Freelance.meServerless.Core;
using rgomezj.Freelance.meServerless.Services.Abstract;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rgomezj.Freelance.meServerless.Services.Implementation
{
    public class SendGridEmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        
        public SendGridEmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendEmail(EmailMessage emailMessage)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(emailMessage.From, emailMessage.FromName),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.Message,
                HtmlContent = emailMessage.HTMLMessage
            };
            msg.AddTo(new EmailAddress(emailMessage.To, emailMessage.ToName));
            await client.SendEmailAsync(msg);
        }
    }
}
