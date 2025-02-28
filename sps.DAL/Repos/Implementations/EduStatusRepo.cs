namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class EduStatusRepo : BaseRepo<EduStatus>, IEduStatusRepo
    {
        public EduStatusRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
