using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PracticePackAPI.Context;
using PracticePackAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;

namespace PracticePackAPI.Repositories
{
    public class GenericRepository<TEntity> : iRepository<TEntity> where TEntity : class, iEntity
    {
        internal ApiContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ApiContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAllWithData(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes(query);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public async Task<TEntity> GetById(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
                query = includes(query);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await _context.AddAsync<TEntity>(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public async Task<bool> Exist(Guid id)
        {
            return await dbSet.AnyAsync(e => e.Id == id);
        }
    }
}