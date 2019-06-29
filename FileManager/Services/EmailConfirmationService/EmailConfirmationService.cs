using FileManager.Models;
using FileManager.Models.EmailSendingOptions;
using System;
using Markdig;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FileManager.Services.EmailConfirmationService
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly EmailSendingOptions _emailSendingOptions;
        public EmailConfirmationService(IOptions<EmailSendingOptions> emailSendingOptions)
        {
            _emailSendingOptions = emailSendingOptions.Value;
        }

        public async Task SendEmailConfirmationAsync(User user,string confirmationLink)
        {

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSendingOptions.Email));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = "Email confirmation";
            message.Body = new TextPart("html")
            {
                Text = Markdown.ToHtml("# Hello\n#### To confirm email click this link - [click here](" + HtmlEncoder.Default.Encode(confirmationLink) + ")")
            };

            using (var client = new SmtpClient())
            {
               // client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(_emailSendingOptions.Host, _emailSendingOptions.Port, _emailSendingOptions.UseSSL);

                client.Authenticate(_emailSendingOptions.Email, _emailSendingOptions.Password);

                await client.SendAsync(message);

                client.Disconnect(true);
            }
        }
    }
}
