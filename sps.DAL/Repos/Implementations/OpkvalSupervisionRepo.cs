namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class OpkvalSupervisionRepo : BaseRepo<OpkvalSupervision>, IOpkvalSupervisionRepo
    {
        public OpkvalSupervisionRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
