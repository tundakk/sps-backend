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

    public class SupportingTeacherService : BaseService<SupportingTeacherModel, SupportingTeacher, ISupportingTeacherRepo>, ISupportingTeacherService
    {
        public SupportingTeacherService(ISupportingTeacherRepo supportingTeacherRepo, ILogger<SupportingTeacherService> logger)
            : base(supportingTeacher, logger)
        {
        }

        // Implement any additional methods specific to SupportingTeacher here
    }
}
