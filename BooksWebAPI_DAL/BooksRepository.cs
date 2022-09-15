using BooksWebAPI_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_DAL
{
    public class BooksRepository : IBooksRepository
    {
        private readonly EFCoreDbContext _dbContext;

        public BooksRepository(EFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id) // Task<(Book, IEnumerable<BookRevision> bookRevisions)> - kortage
        {
            var result = await _dbContext.BookRevisions.Include(x => x.Book).Where(x => x.BookId == id).ToArrayAsync();

            var book = result.FirstOrDefault()?.Book;

            return (book, result);
        }
    }
}
