namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class EduCategoryService : BaseService<EduCategoryModel, EduCategory, IEduCategoryRepo>, IEduCategoryService
    {
        public EduCategoryService(IEduCategoryRepo eduCategoryRepo, ILogger<EduCategoryService> logger)
            : base(eduCategoryRepo, logger)
        {
        }

        // Implement any additional methods specific to EduCategory here
    }
}