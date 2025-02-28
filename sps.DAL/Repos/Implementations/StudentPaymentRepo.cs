namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class StudentPaymentRepo : BaseRepo<StudentPayment>, IStudentPaymentRepo
    {
        public StudentPaymentRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
