namespace sps.BLL.Infrastructure.Services.Implementations
{
    using sps.BLL.Infrastructure.Interfaces;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using Mapster;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;

    public class EduCategoryService : BaseService<EduCategoryModel, EduCategory, IEduCategoryRepo>, IEduCategoryService
    {
        public EduCategoryService(IEduCategoryRepo eduCategoryRepo, ILogger<EduCategoryService> logger)
            : base(eduCategory, logger)
        {
        }

        // Implement any additional methods specific to EduCategory here
    }
}
