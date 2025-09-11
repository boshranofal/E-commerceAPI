using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace E_commerceAPI.Utils
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("boshranofal66@gmail.com", "erfj caqj sjit iuux")
            };

            return client.SendMailAsync(
                new MailMessage(from: "boshranofal66@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                                 { IsBodyHtml=true});
        }
    }
    }

