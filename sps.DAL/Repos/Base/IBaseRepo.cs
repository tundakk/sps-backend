namespace sps.DAL.Repos.Base
{
    /// <summary>
    /// The interface for the base repository class.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Get all entries in the repo.
        /// </summary>
        /// <returns>Returns all instances of a given entity.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>Returns the entity that matches the ID.</returns>
        Task<TEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Add and entity to repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns object that was created.</returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Updates an exising entity in the repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns an updated object of the entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Removes an entity from the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to remove.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Checks if an entity exists in the repository by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to check.</param>
        /// <returns>Returns true if the entity exists, otherwise false.</returns>
        Task<bool> ExistsAsync(Guid id);
    }
}