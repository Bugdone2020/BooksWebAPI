using BooksWebAPI_DAL;
using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_BL
{
    public class BooksService : IBooksService
    {
        private readonly IGenericRepository<Book> _booksRepository;

        public BooksService(IGenericRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Guid> AddBook(Book book)
        {
            return await _booksRepository.Add(book);
        }

        public async Task<bool> DeleteBookById(Guid id)
        {
            return await _booksRepository.DeleteById(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _booksRepository.GetAll();
        }

        public async Task<Book> GetBookById(Guid id)
        {
            return await _booksRepository.GetById(id);
        }

        public async Task<bool> UpdateBook(Book book)
        {
            return await _booksRepository.Update(book);
        }
    }
}
