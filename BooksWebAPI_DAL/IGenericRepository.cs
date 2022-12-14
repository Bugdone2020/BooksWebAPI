using BooksWebAPI_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebAPI_DAL
{
    public interface IGenericRepository<T>
        where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> DeleteById(Guid id);
        Task<bool> Update(T book);
        Task<Guid> Add(T book);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllByPredicate(Expression<Func<T, bool>> predicate);
    }
}
