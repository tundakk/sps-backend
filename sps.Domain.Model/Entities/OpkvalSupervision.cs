namespace sps.Domain.Model.Entities
{
    public class OpkvalSupervision
    {
        public Guid Id { get; set; }
        public string Status { get; set; } // Godkendt, Studerende skal give samtykke, Afventer STUK, Afslag STUK, Anulleret STUK

        public int HoursSpentSupervision { get; set; }
        public int HoursSpentOpkvalificering { get; set; }
        public int HoursSought { get; set; }

        //TODO:
        // maybe make sure that HoursSpentSupervision + HoursSpentOpkvalificering <= HoursSought with business logic

        // Navigation
        public ICollection<SpsaCase> SpsaCases { get; set; }
    }
}