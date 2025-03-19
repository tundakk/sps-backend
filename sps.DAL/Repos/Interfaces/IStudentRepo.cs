namespace sps.DAL.Repos.Interfaces
{
    using sps.DAL.Repos.Base;
    using sps.Domain.Model.Entities;
    using sps.Domain.Model.ValueObjects;

    public interface IStudentRepo : IBaseRepo<Student>
    {
        Task<Student?> GetByCprAsync(CPRNumber cprNumber);

        // Add any additional methods specific to Student if needed
    }
}