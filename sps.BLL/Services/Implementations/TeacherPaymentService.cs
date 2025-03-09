namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class TeacherPaymentService : BaseService<TeacherPaymentModel, TeacherPayment, ITeacherPaymentRepo>, ITeacherPaymentService
    {
        public TeacherPaymentService(ITeacherPaymentRepo teacherPaymentRepo, ILogger<TeacherPaymentService> logger)
            : base(teacherPaymentRepo, logger)
        {
        }

        // Implement any additional methods specific to TeacherPayment here
    }
}