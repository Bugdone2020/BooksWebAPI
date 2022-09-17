using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL
{
    public interface IClientService
    {
        bool RentABook(Book book, User client);

        bool ReturnABook(Book book, User client);
    }
}
