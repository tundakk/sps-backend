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

    public class EduStatusService : BaseService<EduStatusModel, EduStatus, IEduStatusRepo>, IEduStatusService
    {
        public EduStatusService(IEduStatusRepo eduStatusRepo, ILogger<EduStatusService> logger)
            : base(eduStatus, logger)
        {
        }

        // Implement any additional methods specific to EduStatus here
    }
}
