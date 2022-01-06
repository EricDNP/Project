using System.Linq.Expressions;
using PracticePackAPI.Models;

namespace PracticePackAPI.Services
{
    public interface iService<T> where T : class
    {
        ICollection<T> GetAllWithData(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
        );
        Task<ICollection<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid Id);
    }
}