namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class SupportingTeacherRepo : BaseRepo<SupportingTeacher>, ISupportingTeacherRepo
    {
        public SupportingTeacherRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
