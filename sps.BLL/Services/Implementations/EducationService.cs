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

    public class EducationService : BaseService<EducationModel, Education, IEducationRepo>, IEducationService
    {
        public EducationService(IEducationRepo educationRepo, ILogger<EducationService> logger)
            : base(education, logger)
        {
        }

        // Implement any additional methods specific to Education here
    }
}
