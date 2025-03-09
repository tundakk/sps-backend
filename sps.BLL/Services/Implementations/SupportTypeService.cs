namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class SupportTypeService : BaseService<SupportTypeModel, SupportType, ISupportTypeRepo>, ISupportTypeService
    {
        public SupportTypeService(ISupportTypeRepo supportTypeRepo, ILogger<SupportTypeService> logger)
            : base(supportTypeRepo, logger)
        {
        }

        // Implement any additional methods specific to SupportType here
    }
}