namespace sps.BLL.Services.Interfaces
{
    using sps.BLL.Services.Base;
    using sps.Domain.Model.Models;
    using sps.Domain.Model.Responses;
    using sps.Domain.Model.ValueObjects;

    public interface IStudentService : IBaseService<StudentModel>
    {
        public Task<ServiceResponse<StudentModel>> GetByCprAsync(CPRNumber cprNumber);
    }
}