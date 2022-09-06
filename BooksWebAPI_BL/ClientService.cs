using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL
{
    public class ClientService : IClientService
    {
        public bool RentABook(Book book, Client client)
        {
            throw new NotImplementedException();
        }

        public bool ReturnABook(Book book, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
