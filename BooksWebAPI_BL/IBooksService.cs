using System;
using System.Collections.Generic;
using BooksWebAPI_DAL;

namespace BooksWebAPI_BL
{
    public interface IBooksService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(Guid id);
        bool DeleteBookById(Guid id);
        bool UpdateBook(Book book);
        Guid AddBook(Book book);
    }
}
