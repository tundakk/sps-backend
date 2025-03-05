namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class EducationPeriodRateService : BaseService<EducationPeriodRateModel, EducationPeriodRate, IEducationPeriodRateRepo>, IEducationPeriodRateService
    {
        public EducationPeriodRateService(IEducationPeriodRateRepo educationPeriodRateRepo, ILogger<EducationPeriodRateService> logger)
            : base(educationPeriodRateRepo, logger)
        {
        }

        // Implement any additional methods specific to EducationPeriodRate here
    }
}