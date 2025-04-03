using System;

namespace sps.Domain.Model.Entities
{
    public class EducationPeriodRate
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        // FKs
        public Guid PeriodId { get; set; }
        public Period Period { get; set; }

        // Update property names to be consistent
        public Guid EducationalProgramId { get; set; }
        public EducationalProgram EducationalProgram { get; set; }
    }
}