using Filmix.Models.EmailModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<bool> SendConfirmEmailAsync(string email,string token,string userId)
        {
            var config = GetConfiguration();
            using var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Filmix", config.SmtpEmail));
            mimeMessage.To.Add(new MailboxAddress(" ", email));
            mimeMessage.Subject = "Подтверждение регистрации";
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"<a style='font-size:30px;text-decoration:none;color:#b70c0c' href='https://localhost:44343/ConfirmEmail?token={token}&userId={userId}'>Для подтверждения регистрации нажмите сюда</a>" };
            
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

        public async Task SendSuccessRegisterEmailAsync(string email)
        {
            var config = GetConfiguration();
            using var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Filmix", config.SmtpEmail));
            mimeMessage.To.Add(new MailboxAddress(" ", email));
            mimeMessage.Subject = "Ваша учетная запись успешно подтверждена!";
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"<h1 style='font-size:30px; color:#095b99'>Спасибо за регистрацию в Filmix!</h1>" };

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
            catch(Exception ex)
            {
                 Debug.Fail(ex.Message);
            }


        }
    }
}
