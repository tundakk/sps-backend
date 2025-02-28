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

    public class StudentPaymentService : BaseService<StudentPaymentModel, StudentPayment, IStudentPaymentRepo>, IStudentPaymentService
    {
        public StudentPaymentService(IStudentPaymentRepo studentPaymentRepo, ILogger<StudentPaymentService> logger)
            : base(studentPayment, logger)
        {
        }

        // Implement any additional methods specific to StudentPayment here
    }
}
