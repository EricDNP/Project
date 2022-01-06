using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using PracticePackAPI.Models;

namespace PracticePackAPI.Repositories
{
    public interface iRepository<T> where T : iEntity
    {
        Task<ICollection<T>> GetAll();

        IEnumerable<T> GetAllWithData(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task<T> GetById(
            Guid id,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> Exist(Guid id);
    }
}