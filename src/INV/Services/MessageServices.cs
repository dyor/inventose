using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using System.Net.Mail;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using INV.Models.AccountViewModels;

namespace INV.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {

        public Task SendEmailAsync(string email, string subject, string message)
        {

            var thismessage = new MimeMessage();
            thismessage.From.Add(new MailboxAddress("Matt Dyor", "matt.dyor@inventose.com"));
            thismessage.To.Add(new MailboxAddress(email, email));
            thismessage.Subject = subject;
            thismessage.Body = new TextPart("plain")
            {
                Text = message
            };
            
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism. 
                Environment.GetEnvironmentVariable("SmtpUsername");


                client.Authenticate(Environment.GetEnvironmentVariable("SmtpUsername"), Environment.GetEnvironmentVariable("SmtpPassword"));

                client.Send(thismessage);
                client.Disconnect(true);
            }

            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
