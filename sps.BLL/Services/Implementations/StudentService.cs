namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;

    public class StudentService : BaseService<StudentModel, Student, IStudentRepo>, IStudentService
    {
        public StudentService(IStudentRepo studentRepo, ILogger<StudentService> logger)
            : base(studentRepo, logger)
        {
        }

        // Implement any additional methods specific to Student here
    }
}