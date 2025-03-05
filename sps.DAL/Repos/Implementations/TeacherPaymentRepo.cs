using sps.DAL.DataModel;
using sps.DAL.Repos.Base;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Entities;

namespace sps.DAL.Repos.Implementations
{
    public class TeacherPaymentRepo : BaseRepo<TeacherPayment>, ITeacherPaymentRepo
    {
        public TeacherPaymentRepo(SpsDbContext dataContext) : base(dataContext)
        {
        }
    }
}