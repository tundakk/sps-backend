namespace sps.DAL.Repos
{
    using sps.DAL.DataModel;
    using sps.DAL.Entities;
    using sps.DAL.Repos.Base;
    using sps.DAL.Repos.Interfaces;

    public class DiagnosisRepo : BaseRepo<Diagnosis>, IDiagnosisRepo
    {
        public DiagnosisRepo(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
