namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class SupportingTeacherService : BaseService<SupportingTeacherModel, SupportingTeacher, ISupportingTeacherRepo>, ISupportingTeacherService
    {
        public SupportingTeacherService(ISupportingTeacherRepo supportingTeacherRepo, ILogger<SupportingTeacherService> logger)
            : base(supportingTeacherRepo, logger)
        {
        }

        // Implement any additional methods specific to SupportingTeacher here
    }
}