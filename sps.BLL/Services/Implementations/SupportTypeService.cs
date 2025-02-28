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

    public class SupportTypeService : BaseService<SupportTypeModel, SupportType, ISupportTypeRepo>, ISupportTypeService
    {
        public SupportTypeService(ISupportTypeRepo supportTypeRepo, ILogger<SupportTypeService> logger)
            : base(supportType, logger)
        {
        }

        // Implement any additional methods specific to SupportType here
    }
}
