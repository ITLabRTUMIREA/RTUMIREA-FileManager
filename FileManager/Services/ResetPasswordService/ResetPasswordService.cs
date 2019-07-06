using FileManager.Models.EmailSendingOptions;
using MailKit.Net.Smtp;
using Markdig;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FileManager.Models;
using Microsoft.AspNetCore.Mvc;
using FileManager.Models.Database.UserDepartmentRoles;

namespace FileManager.Services.ResetPasswordService
{
    public class ResetPasswordService: IResetPasswordService
    {
        private readonly EmailSendingOptions _emailSendingOptions;
        public ResetPasswordService(IOptions<EmailSendingOptions> emailSendingOptions)
        {
            _emailSendingOptions = emailSendingOptions.Value;
        }
        public async Task SendResetPasswordConfirmationLinkToEmailAsync(User user, string resetPasswordCallbackLink)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSendingOptions.Email));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = "Password reset";
            message.Body = new TextPart("html")
            {
                Text = Markdown.ToHtml("# Hello\n#### To confirm reset password click this link - [click here](" + HtmlEncoder.Default.Encode(resetPasswordCallbackLink) + ")")
            };

            using (var client = new SmtpClient())
            {

                client.Connect(_emailSendingOptions.Host, _emailSendingOptions.Port, _emailSendingOptions.UseSSL);

                client.Authenticate(_emailSendingOptions.Email, _emailSendingOptions.Password);

                await client.SendAsync(message);

                client.Disconnect(true);
            }

        }
    }
}
