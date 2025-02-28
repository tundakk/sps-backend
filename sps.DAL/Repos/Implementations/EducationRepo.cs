namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class EducationRepo : BaseRepo<Education>, IEducationRepo
    {
        public EducationRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
