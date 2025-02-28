using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;

namespace sps.DAL.Repos.Base
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepo(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await GetByIdAsync(id) != null;
        }
    }
}