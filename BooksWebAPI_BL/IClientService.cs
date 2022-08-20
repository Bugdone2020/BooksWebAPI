using BooksWebAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL
{
    public interface IClientService
    {
        bool RentABook(BooksService book, Client client);

        bool ReturnABook(Book book, Client client);
    }
}
