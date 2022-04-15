using Filmix.Models.EmailModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Filmix.Services
{
    public class EmailService : IEmailService
    {
        public IConfiguration Configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        public EmailConfiguration GetConfiguration()
        {
            
            EmailConfiguration emailConfig = new EmailConfiguration();
            emailConfig.SmtpServer = Configuration.GetEmailConfiguration("SmtpServer");
            emailConfig.SmtpPort   = int.Parse(Configuration.GetEmailConfiguration("SmtpPort"));
            emailConfig.SmtpEmail = Configuration.GetEmailConfiguration("SmtpEmail");
            emailConfig.SmtpPassword = Configuration.GetEmailConfiguration("SmtpPassword");

            return emailConfig;
        }

        public async Task<bool> SendEmailAsync(string email,string token)
        {
            var config = GetConfiguration();
            using var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Filmix", config.SmtpEmail));
            mimeMessage.To.Add(new MailboxAddress(" ", email));
            mimeMessage.Subject = "Подтверждение регистрации";
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"<a href='https://localhost:44343/ConfirmEmail?token={token}'>Жмякай сюда</a>" };
            
            try
            {
                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(config.SmtpServer, config.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(config.SmtpEmail, config.SmtpPassword);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch
            {
                return false;
            
            }
            
            return true;

        }
    }
}
