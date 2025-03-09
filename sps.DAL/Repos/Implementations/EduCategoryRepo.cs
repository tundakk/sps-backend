using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class EduCategoryRepo : BaseRepo<EduCategory>, IEduCategoryRepo
    {
        public EduCategoryRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}