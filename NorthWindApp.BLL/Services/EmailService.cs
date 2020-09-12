using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NorthWindApp.BLL.Infrastructure;

namespace NorthWindApp.BLL.Services
{
    public class EmailService: IEmailService
    {
        private readonly string smtpHost = "smtp.yandex.ru";
        private readonly int smtpPort = 25;
        // You should configure login and password in Secret Manager Tool
        // dotnet user-secrets set "smtpLogin" "<login@yandex.ru>" --project D:\PROJECTS\CSHARP\AspMentoringCourse\Project\NorthWindApp
        // dotnet user-secrets set "smtpPassword" "<password>" --project D:\PROJECTS\CSHARP\AspMentoringCourse\Project\NorthWindApp
        private readonly string _userName;
        private readonly string _password;

        public EmailService(IConfiguration configuration)
        {
            _userName = configuration["smtpLogin"];
            _password = configuration["smtpPassword"];
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin NorthWind site", _userName));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpHost, smtpPort, false);
                await client.AuthenticateAsync(_userName, _password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
