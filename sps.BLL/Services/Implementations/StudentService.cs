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

    public class StudentService : BaseService<StudentModel, Student, IStudentRepo>, IStudentService
    {
        public StudentService(IStudentRepo studentRepo, ILogger<StudentService> logger)
            : base(student, logger)
        {
        }

        // Implement any additional methods specific to Student here
    }
}
