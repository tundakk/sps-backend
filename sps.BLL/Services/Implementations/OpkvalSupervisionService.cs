namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class OpkvalSupervisionService : BaseService<OpkvalSupervisionModel, OpkvalSupervision, IOpkvalSupervisionRepo>, IOpkvalSupervisionService
    {
        public OpkvalSupervisionService(IOpkvalSupervisionRepo opkvalSupervisionRepo, ILogger<OpkvalSupervisionService> logger)
            : base(opkvalSupervisionRepo, logger)
        {
        }

        // Implement any additional methods specific to OpkvalSupervision here
    }
}