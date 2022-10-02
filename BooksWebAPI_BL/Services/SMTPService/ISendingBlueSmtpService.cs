using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.SMTPService
{
    public interface ISendingBlueSmtpService
    {
        Task SendMail(MailInfo mailInfo);
    }
}
