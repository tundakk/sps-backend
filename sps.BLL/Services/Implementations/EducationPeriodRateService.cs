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

    public class EducationPeriodRateService : BaseService<EducationPeriodRateModel, EducationPeriodRate, IEducationPeriodRateRepo>, IEducationPeriodRateService
    {
        public EducationPeriodRateService(IEducationPeriodRateRepo educationPeriodRateRepo, ILogger<EducationPeriodRateService> logger)
            : base(educationPeriodRate, logger)
        {
        }

        // Implement any additional methods specific to EducationPeriodRate here
    }
}
