namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class StudentPaymentService : BaseService<StudentPaymentModel, StudentPayment, IStudentPaymentRepo>, IStudentPaymentService
    {
        public StudentPaymentService(IStudentPaymentRepo studentPaymentRepo, ILogger<StudentPaymentService> logger)
            : base(studentPaymentRepo, logger)
        {
        }

        // Implement any additional methods specific to StudentPayment here
    }
}