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

    public class SpsaCaseService : BaseService<SpsaCaseModel, SpsaCase, ISpsaCaseRepo>, ISpsaCaseService
    {
        public SpsaCaseService(ISpsaCaseRepo spsaCaseRepo, ILogger<SpsaCaseService> logger)
            : base(spsaCase, logger)
        {
        }

        // Implement any additional methods specific to SpsaCase here
    }
}
