namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class SpsaCaseService : BaseService<SpsaCaseModel, SpsaCase, ISpsaCaseRepo>, ISpsaCaseService
    {
        public SpsaCaseService(ISpsaCaseRepo spsaCaseRepo, ILogger<SpsaCaseService> logger)
            : base(spsaCaseRepo, logger)
        {
        }

        // Implement any additional methods specific to SpsaCase here
    }
}