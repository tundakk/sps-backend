namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class DiagnosisService : BaseService<DiagnosisModel, Diagnosis, IDiagnosisRepo>, IDiagnosisService
    {
        public DiagnosisService(IDiagnosisRepo diagnosisRepo, ILogger<DiagnosisService> logger)
            : base(diagnosisRepo, logger)
        {
        }

        // Implement any additional methods specific to Diagnosis here
    }
}