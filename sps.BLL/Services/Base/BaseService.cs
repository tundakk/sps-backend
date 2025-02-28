using Mapster;
using Microsoft.Extensions.Logging;
using sps.DAL.Repos.Base;

namespace sps.BLL.Infrastructure.Services.Base
{
    public abstract class BaseService<TModel, TEntity, TRepo> : IBaseService<TModel>
        where TModel : class
        where TEntity : class
        where TRepo : IBaseRepo<TEntity>
    {
        protected readonly TRepo _repository;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseService(TRepo repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = TypeAdapter.Getter;
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => _mapper.Map<TModel>(e));
        }

        public virtual async Task<TModel?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity != null ? _mapper.Map<TModel>(entity) : null;
        }

        public virtual async Task<TModel> InsertAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.InsertAsync(entity);
            return _mapper.Map<TModel>(result);
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var result = await _repository.UpdateAsync(entity);
            return _mapper.Map<TModel>(result);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}