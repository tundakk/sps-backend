using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class PeriodRepo : BaseRepo<Period>, IPeriodRepo
    {
        public PeriodRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}