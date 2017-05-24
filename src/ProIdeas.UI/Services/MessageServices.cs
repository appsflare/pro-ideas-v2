using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElasticEmail.WebApiClient;

namespace ProIdeas.UI.Services
{
    public class AuthMessageSenderOptions
    {
        public string ElasticEmailApiKey { get; set; }
        public string DefaultSenderEmail { get; set; }
        public string DefaultSenderName { get; set; }
    }

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly AuthMessageSenderOptions _options;
        private readonly ILogger _logger;
        private readonly EasyMail _mailClient;
        public AuthMessageSender(AuthMessageSenderOptions options, ILoggerFactory loggerFactory)
        {
            _options = options;
            _logger = loggerFactory.CreateLogger<AuthMessageSender>();
            _mailClient = new EasyMail(_options.ElasticEmailApiKey);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                _logger.LogInformation("Sending mail to: {0}, subject: {1}", email, subject);

                var recipient = new TransactionalRecipient
                {
                    To = new List<Email>
                    {
                        new Email(email,"dummy")
                    }
                };
                var defaultEmail = new Email(_options.DefaultSenderEmail, _options.DefaultSenderName);

                var mailMessage = new Message
                {
                    Subject = subject,
                    BodyHtml = message,
                    Sender = defaultEmail,
                    From = defaultEmail,
                    ReplyTo = defaultEmail,
                    FromMimeHeader = defaultEmail
                };

                var result = await _mailClient.SendTransactionalMailAsync(recipient, mailMessage);

                _logger.LogInformation("Sent mail to: {0}, subject: {1}, message id: {2}, transaction id: {3}", email, subject, result.MessageID, result.TransactionID);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                throw;
            }


        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
