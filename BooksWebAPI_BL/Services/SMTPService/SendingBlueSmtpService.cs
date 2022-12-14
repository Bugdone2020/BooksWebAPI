using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.SMTPService
{
    public class SendingBlueSmtpService : ISendingBlueSmtpService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public SendingBlueSmtpService(IOptions<SmtpConfiguration> options)
        {
            _smtpConfiguration = options.Value;
        }

        public async Task SendMail(MailInfo mailInfo)
        {
            using (var SmtpServer = new SmtpClient
            {
                Host = _smtpConfiguration.Host,
                Port = _smtpConfiguration.Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    _smtpConfiguration.SenderMail,
                    _smtpConfiguration.SenderPassword),
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            })
            {
                var fromMessage = new MailAddress(
                    _smtpConfiguration.SenderMail,
                    _smtpConfiguration.SenderName);
                var toMessage = new MailAddress(
                    mailInfo.Email,//input"climate2you@gmail.com"
                    mailInfo.ClientName);//input"Oleg"
                using (MailMessage message = new MailMessage
                {
                    From = fromMessage,
                    Subject = mailInfo.Subject,//"Test mail",//input
                    Body = mailInfo.Body//"This is for testing SMTP mail from GMAIL"//input
                })
                {
                    message.To.Add(toMessage);

                    await SmtpServer.SendMailAsync(message);
                }
            }
        }
    }
}
