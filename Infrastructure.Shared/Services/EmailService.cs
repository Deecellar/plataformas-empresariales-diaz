using Application.DTOs.Email;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings MailSettings { get; }
        public ILogger<EmailService> Logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings,ILogger<EmailService> logger)
        {
            MailSettings = mailSettings.Value;
            Logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                // create message
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom)
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder
                {
                    HtmlBody = request.Body
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}
