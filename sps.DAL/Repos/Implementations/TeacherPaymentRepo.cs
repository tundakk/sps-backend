namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class TeacherPaymentRepo : BaseRepo<TeacherPayment>, ITeacherPaymentRepo
    {
        public TeacherPaymentRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
