namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class EduStatusService : BaseService<EduStatusModel, EduStatus, IEduStatusRepo>, IEduStatusService
    {
        public EduStatusService(IEduStatusRepo eduStatusRepo, ILogger<EduStatusService> logger)
            : base(eduStatusRepo, logger)
        {
        }

        // Implement any additional methods specific to EduStatus here
    }
}