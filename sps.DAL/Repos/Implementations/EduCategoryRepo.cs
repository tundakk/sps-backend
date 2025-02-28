namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class EduCategoryRepo : BaseRepo<EduCategory>, IEduCategoryRepo
    {
        public EduCategoryRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
