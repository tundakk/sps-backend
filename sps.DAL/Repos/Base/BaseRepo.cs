using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;

namespace sps.DAL.Repos.Base
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly SpsDbContext _context;

        protected BaseRepo(SpsDbContext context)
        {
            _context = context;
        }        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>Returns the entity that matches the ID, or null if not found.</returns>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Returns all instances of a given entity.</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            // Use 'await' with 'ToListAsync' to get the result asynchronously
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the object that was created.</returns>
        public virtual async Task<T> InsertAsync(T entity)
        {
            // Use 'await' with 'AddAsync'
            var addedEntity = (await _context.Set<T>().AddAsync(entity)).Entity;
            await SaveAsync();
            return addedEntity;
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the updated object of the entity.</returns>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Update - Entity must not be null");
            }

            // Use 'Update' (no async version available)
            _context.Set<T>().Update(entity);
            await SaveAsync();
            return entity;
        }

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the object that was removed.</returns>
        public virtual async Task<T> DeleteAsync(T entity)
        {
            var removedEntity = _context.Set<T>().Remove(entity).Entity;
            await SaveAsync();
            return removedEntity;
        }

        /// <summary>
        /// Saves changes to the repository.
        /// </summary>
        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var newEx = new Exception($"DAL SaveAsync - Could not be completed: {ex.Message}.", ex);
                throw newEx;
            }
        }
    }
}