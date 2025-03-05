namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class PeriodService : BaseService<PeriodModel, Period, IPeriodRepo>, IPeriodService
    {
        public PeriodService(IPeriodRepo periodRepo, ILogger<PeriodService> logger)
            : base(periodRepo, logger)
        {
        }

        // Implement any additional methods specific to Period here
    }
}