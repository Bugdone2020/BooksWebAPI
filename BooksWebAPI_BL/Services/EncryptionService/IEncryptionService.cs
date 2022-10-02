using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL.Services.EncryptionService
{
    public interface IEncryptionService
    {
        string EncryptString(string plainText);
        string DecryptString(string cipherText);
    }
}
