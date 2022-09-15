using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksWebAPI_DAL
{
    public interface IBooksRepository
    {
        Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id);
    }
}