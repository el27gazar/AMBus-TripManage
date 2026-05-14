using AMBus.TripManage.Application.Contracts.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace AMBus.TripManage.Persistance.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var senderPassword = _config["EmailSettings:SenderPassword"];
            var useSSL = bool.Parse(_config["EmailSettings:UseSSL"]);

            using var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = useSSL,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            var message = new MailMessage(senderEmail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
            Console.WriteLine($"✅ Email sent to {to}");
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            var subject = "Confirm Your Email - AMBus Trip Manager";
            var body = $@"
                <h2>Welcome to AMBus Trip Manager!</h2>
                <p>Please confirm your email by clicking the link below:</p>
                <a href='{confirmationLink}'>Confirm Email</a>
                <p>Or copy this link: {confirmationLink}</p>
                <p>This link will expire in 24 hours.</p>
            ";

            await SendEmailAsync(email, subject, body);
        }
    }
}