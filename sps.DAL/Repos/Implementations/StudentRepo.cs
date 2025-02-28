namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class StudentRepo : BaseRepo<Student>, IStudentRepo
    {
        public StudentRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
