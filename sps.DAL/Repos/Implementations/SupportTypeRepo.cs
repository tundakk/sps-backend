namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class SupportTypeRepo : BaseRepo<SupportType>, ISupportTypeRepo
    {
        public SupportTypeRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
