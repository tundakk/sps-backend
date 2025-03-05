namespace sps.Domain.Model.Entities
{
    public class EducationPeriodRate
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        // FKs
        public Guid PeriodId { get; set; }

        public Period Period { get; set; }

        public Guid EducationId { get; set; }
        public Education Education { get; set; }
    }
}