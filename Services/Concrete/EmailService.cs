using System.Linq;
using System.Threading.Tasks;
using dytsenayasar.Models.EmailManager;
using dytsenayasar.Models.Settings;
using dytsenayasar.Services.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace dytsenayasar.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        private readonly EmailManagerSettings _settings;

        public EmailService(ILogger<EmailService> logger, IOptions<EmailManagerSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task Send(EmailMessageModel message)
        {
            try
            {
                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderName));
                msg.To.AddRange(message.ToAdresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                msg.Cc.AddRange(message.CCAdresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                msg.Subject = message.Subject;
                msg.Body = new TextPart(TextFormat.Html)
                {
                    Text = message.Content
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(_settings.SmtpUsername, _settings.SmtpPassword);
                    await client.SendAsync(msg);
                    await client.DisconnectAsync(true);
                }
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, "Email send error!");
            }
        }
    }
}