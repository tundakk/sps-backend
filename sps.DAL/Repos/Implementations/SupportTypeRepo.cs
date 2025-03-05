using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class SupportTypeRepo : BaseRepo<SupportType>, ISupportTypeRepo
    {
        public SupportTypeRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}