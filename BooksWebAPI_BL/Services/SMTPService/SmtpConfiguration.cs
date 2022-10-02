using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.SMTPService
{
    public class SmtpConfiguration
    {
        public string SenderMail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
