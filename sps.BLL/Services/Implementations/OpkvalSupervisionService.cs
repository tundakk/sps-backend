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

    public class OpkvalSupervisionService : BaseService<OpkvalSupervisionModel, OpkvalSupervision, IOpkvalSupervisionRepo>, IOpkvalSupervisionService
    {
        public OpkvalSupervisionService(IOpkvalSupervisionRepo opkvalSupervisionRepo, ILogger<OpkvalSupervisionService> logger)
            : base(opkvalSupervision, logger)
        {
        }

        // Implement any additional methods specific to OpkvalSupervision here
    }
}
