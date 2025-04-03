namespace sps.BLL.Services.Implementations
{
    using Mapster;
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

            // Use Mapster to map the entity to model
            var studentModel = student.Adapt<StudentModel>();
            return ServiceResponse<StudentModel>.CreateSuccess(studentModel);
        }

        public override async Task<ServiceResponse<StudentModel>> UpdateAsync(StudentModel model)
        {
            var entity = await Repository.GetByIdAsync(model.Id);
            if (entity == null)
            {
                return ServiceResponse<StudentModel>.CreateNotFound("Student not found");
            }

            // Use Mapster to map properties from model to entity
            // Excluding properties that shouldn't be overwritten
            model.Adapt(entity);
            
            // Make sure to set the UpdatedAt timestamp
            entity.UpdatedAt = DateTime.UtcNow;

            var updatedEntity = await Repository.UpdateAsync(entity);
            return ServiceResponse<StudentModel>.CreateSuccess(updatedEntity.Adapt<StudentModel>());
        }
    }
}