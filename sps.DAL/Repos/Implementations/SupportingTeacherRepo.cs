using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class SupportingTeacherRepo : BaseRepo<SupportingTeacher>, ISupportingTeacherRepo
    {
        public SupportingTeacherRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}