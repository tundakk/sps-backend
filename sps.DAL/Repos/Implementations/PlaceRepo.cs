using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class PlaceRepo : BaseRepo<Place>, IPlaceRepo
    {
        public PlaceRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}