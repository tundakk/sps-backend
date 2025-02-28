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

    public class TeacherPaymentService : BaseService<TeacherPaymentModel, TeacherPayment, ITeacherPaymentRepo>, ITeacherPaymentService
    {
        public TeacherPaymentService(ITeacherPaymentRepo teacherPaymentRepo, ILogger<TeacherPaymentService> logger)
            : base(teacherPayment, logger)
        {
        }

        // Implement any additional methods specific to TeacherPayment here
    }
}
