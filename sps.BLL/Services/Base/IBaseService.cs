namespace sps.BLL.Infrastructure.Services.Base
{
    public interface IBaseService<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel?> GetByIdAsync(Guid id);
        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}