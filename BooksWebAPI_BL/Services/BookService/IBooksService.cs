using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BooksWebAPI_BL.DTOs;
using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;

namespace BooksWebAPI_BL.Services.BookService
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(Guid id);
        Task<BookDto> GetBookFullInfo(Guid id);
        Task<bool> DeleteBookById(Guid id);
        Task<bool> UpdateBook(Book book);
        Task<Guid> AddBook(Book book);
    }
}
