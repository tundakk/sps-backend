namespace sps.BLL.Services.Implementations
{
    using Microsoft.Extensions.Logging;
    using sps.BLL.Services.Base;
    using sps.BLL.Services.Interfaces;
    using sps.DAL.Repos.Interfaces;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using sps.Domain.Model.ValueObjects;
    using System.Linq;
    using System.Threading.Tasks;

    public class StudentService : BaseService<StudentModel, Student, IStudentRepo>, IStudentService
    {
        public StudentService(IStudentRepo studentRepo, ILogger<StudentService> logger)
            : base(studentRepo, logger)
        {
        }

        public async Task<ServiceResponse<StudentModel>> GetByCprAsync(CPRNumber cprNumber)
        {
            var student = await Repository.GetByCprAsync(cprNumber);
            if (student == null)
            {
                return ServiceResponse<StudentModel>.CreateNotFound("Student not found");
            }

            var studentModel = new StudentModel
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                CPRNumber = student.CPRNumber,
                Name = student.Name,
                FinishedDate = student.FinishedDate,
                StartPeriodId = student.StartPeriodId,
                EducationId = student.EducationId,
                Comments = student.Comments.Select(c => new CommentModel
                {
                    Id = c.Id,
                    CommentText = c.CommentText,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = c.CreatedBy,
                    EntityType = c.EntityType,
                    EntityId = c.StudentId ?? c.SpsaCaseId ?? c.TeacherPaymentId ?? c.StudentPaymentId ?? c.OpkvalSupervisionId ?? Guid.Empty
                }).ToList(),
                SpsaCases = student.SpsaCases.Select(sc => new SpsaCaseModel
                {
                    Id = sc.Id,
                    SpsaCaseNumber = sc.SpsaCaseNumber,
                    ApplicationDate = sc.ApplicationDate,
                    LatestReapplicationDate = sc.LatestReapplicationDate,
                    StudentId = sc.StudentId,
                    SupportingTeacherId = sc.SupportingTeacherId,
                    AppliedPeriodId = sc.AppliedPeriodId,
                    DiagnosisId = sc.DiagnosisId,
                    EduCategoryId = sc.EduCategoryId,
                    SupportTypeId = sc.SupportTypeId,
                    EduStatusId = sc.EduStatusId,
                    TeacherPaymentId = sc.TeacherPaymentId,
                    OpkvalSupervisionId = sc.OpkvalSupervisionId,
                    StudentPaymentId = sc.StudentPaymentId
                }).ToList()
            };
            return ServiceResponse<StudentModel>.CreateSuccess(studentModel);
        }
    }
}