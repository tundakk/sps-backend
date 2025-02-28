namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class PeriodRepo : BaseRepo<Period>, IPeriodRepo
    {
        public PeriodRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
