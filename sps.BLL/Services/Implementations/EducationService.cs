namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class EducationService : BaseService<EducationModel, Education, IEducationRepo>, IEducationService
    {
        public EducationService(IEducationRepo educationRepo, ILogger<EducationService> logger)
            : base(educationRepo, logger)
        {
        }

        // Implement any additional methods specific to Education here
    }
}