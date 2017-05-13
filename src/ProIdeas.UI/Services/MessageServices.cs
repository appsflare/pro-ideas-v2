using Newtonsoft.Json;
using ProIdeas.Serializers.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProIdeas.UI.Services
{
    public class AuthMessageSenderOptions
    {
        public string MailJetApiKey { get; set; }

        public string MailJetApiSecret { get; set; }

        public string DefaultSenderEmail { get; set; }
        public string DefaultSenderName { get; set; }
    }

    internal class MailRecipient
    {
        public string Email { get; set; }
    }

    internal class MailMessage
    {
        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string FromName { get; set; }

        [JsonProperty("Html_part")]
        public string Html { get; set; }

        public IEnumerable<MailRecipient> Recepients { get; set; }

    }

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly AuthMessageSenderOptions _options;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly AuthenticationHeaderValue _authorizationHeader;
        public AuthMessageSender(AuthMessageSenderOptions options, IJsonSerializer jsonSerializer)
        {
            _options = options;
            _jsonSerializer = jsonSerializer;
            _authorizationHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.MailJetApiKey}:{options.MailJetApiSecret}")));
        }

        async public Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new HttpClient())
            {

                var emailMessage = new MailMessage
                {
                    FromEmail = _options.DefaultSenderEmail,
                    FromName = _options.DefaultSenderName,
                    Html = message,
                    Recepients = new[] { new MailRecipient
                    {
                        Email = email
                    }}
                };

                client.DefaultRequestHeaders.Authorization = _authorizationHeader;

                var messageContent = _jsonSerializer.Serialize(emailMessage);

                await client.PostAsync("https://api.mailjet.com/v3/send", new StringContent(messageContent, Encoding.UTF8, "application/json"));
            }


        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
