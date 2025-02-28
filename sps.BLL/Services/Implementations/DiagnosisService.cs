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

    public class DiagnosisService : BaseService<DiagnosisModel, Diagnosis, IDiagnosisRepo>, IDiagnosisService
    {
        public DiagnosisService(IDiagnosisRepo diagnosisRepo, ILogger<DiagnosisService> logger)
            : base(diagnosis, logger)
        {
        }

        // Implement any additional methods specific to Diagnosis here
    }
}
