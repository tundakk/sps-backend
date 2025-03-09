using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class SpsaCaseRepo : BaseRepo<SpsaCase>, ISpsaCaseRepo
    {
        public SpsaCaseRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}