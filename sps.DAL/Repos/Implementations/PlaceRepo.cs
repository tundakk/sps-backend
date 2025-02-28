namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class PlaceRepo : BaseRepo<Place>, IPlaceRepo
    {
        public PlaceRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
