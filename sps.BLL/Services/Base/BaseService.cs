using Mapster;
using Microsoft.Extensions.Logging;
using sps.DAL.Repos.Base;
using sps.Domain.Model.Responses;

namespace sps.BLL.Services.Base
{
    /// <summary>
    /// Base service class providing common CRUD operations.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TRepo">The type of the repository.</typeparam>
    public abstract class BaseService<TModel, TEntity, TRepo> : IBaseService<TModel>
        where TModel : class
        where TEntity : class
        where TRepo : IBaseRepo<TEntity>
    {
        /// <summary>
        /// The repository instance.
        /// </summary>
        protected readonly TRepo Repository;

        /// <summary>
        /// The logger instance.
        /// </summary>
        protected readonly ILogger<BaseService<TModel, TEntity, TRepo>> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TModel, TEntity, TRepo}"/> class.
        /// </summary>
        /// <param name="repository">The repository instance.</param>
        /// <param name="logger">The logger instance.</param>
        protected BaseService(TRepo repository, ILogger<BaseService<TModel, TEntity, TRepo>> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        /// <inheritdoc />
        public virtual async Task<ServiceResponse<IEnumerable<TModel>>> GetAllAsync()
        {
            try
            {
                var entities = await Repository.GetAllAsync();
                var models = entities.Adapt<IEnumerable<TModel>>();
                return ServiceResponse<IEnumerable<TModel>>.CreateSuccess(models);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error getting all entities");
                return ServiceResponse<IEnumerable<TModel>>.CreateError(
                    ex.Message, 
                    "REPOSITORY_ERROR");
            }
        }

        /// <inheritdoc />
        public virtual async Task<ServiceResponse<TModel>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return ServiceResponse<TModel>.CreateNotFound($"Entity with ID {id} not found");
                }

                var model = entity.Adapt<TModel>();
                return ServiceResponse<TModel>.CreateSuccess(model);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error getting entity by id");
                return ServiceResponse<TModel>.CreateError(
                    ex.Message,
                    "REPOSITORY_ERROR");
            }
        }

        /// <inheritdoc />
        public virtual async Task<ServiceResponse<TModel>> InsertAsync(TModel model)
        {
            try
            {
                // Add validation logic here if needed
                if (model == null)
                {
                    return ServiceResponse<TModel>.CreateError(
                        "Model cannot be null",
                        "VALIDATION_ERROR");
                }
                
                var entity = model.Adapt<TEntity>();
                var insertedEntity = await Repository.InsertAsync(entity);
                var insertedModel = insertedEntity.Adapt<TModel>();
                return ServiceResponse<TModel>.CreateSuccess(insertedModel);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error inserting entity");
                return ServiceResponse<TModel>.CreateError(
                    ex.Message,
                    "REPOSITORY_ERROR");
            }
        }

        /// <inheritdoc />
        public virtual async Task<ServiceResponse<TModel>> UpdateAsync(TModel model)
        {
            try
            {
                // Add validation logic here if needed
                if (model == null)
                {
                    return ServiceResponse<TModel>.CreateError(
                        "Model cannot be null",
                        "VALIDATION_ERROR");
                }
                
                var entity = model.Adapt<TEntity>();
                var updatedEntity = await Repository.UpdateAsync(entity);
                var updatedModel = updatedEntity.Adapt<TModel>();
                return ServiceResponse<TModel>.CreateSuccess(updatedModel);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating entity");
                return ServiceResponse<TModel>.CreateError(
                    ex.Message,
                    "REPOSITORY_ERROR");
            }
        }

        /// <inheritdoc />
        public virtual async Task<ServiceResponse<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(id);
                if (entity == null)
                {
                    Logger.LogWarning("Entity with ID {Id} not found.", id);
                    return ServiceResponse<bool>.CreateNotFound($"Entity with ID {id} not found");
                }

                await Repository.DeleteAsync(entity); // Perform the delete
                return ServiceResponse<bool>.CreateSuccess(true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error deleting entity with ID {Id}", id);
                return ServiceResponse<bool>.CreateError(
                    ex.Message,
                    "REPOSITORY_ERROR");
            }
        }
    }
}