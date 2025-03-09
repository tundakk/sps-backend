namespace sps.Domain.Model.Entities
{
    public class OpkvalSupervision
    {
        public OpkvalSupervision()
        {
            SpsaCases = new HashSet<SpsaCase>();
            Comments = new HashSet<OpkvalSupervisionComment>();
        }

        public Guid Id { get; set; }
        public required DateTime Date { get; set; }
        public string SupervisorName { get; set; }
        public string Notes { get; set; }
        public string Outcome { get; set; }
        public int HoursSought { get; set; }
        public int QualificationHoursSpent { get; set; }
        public int SupervisionHoursSpent { get; set; }
        public string Status { get; set; }
        
        public ICollection<SpsaCase> SpsaCases { get; init; }
        public ICollection<OpkvalSupervisionComment> Comments { get; init; }
    }
}