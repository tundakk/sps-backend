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

    public class PeriodService : BaseService<PeriodModel, Period, IPeriodRepo>, IPeriodService
    {
        public PeriodService(IPeriodRepo periodRepo, ILogger<PeriodService> logger)
            : base(period, logger)
        {
        }

        // Implement any additional methods specific to Period here
    }
}
