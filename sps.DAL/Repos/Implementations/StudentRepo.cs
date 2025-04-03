using Microsoft.EntityFrameworkCore;
using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Repos.Implementations
{
    public class StudentRepo : BaseRepo<Student>, IStudentRepo
    {
        public StudentRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<Student?> GetByCprAsync(CPRNumber cprNumber)
        {
            return await _context.Students
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(s => s.CPRNumber == cprNumber);
        }

        public override async Task<Student> UpdateAsync(Student entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            return await base.UpdateAsync(entity);
        }
    }
}