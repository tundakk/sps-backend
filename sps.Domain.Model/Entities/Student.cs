using System;
using System.Collections.Generic;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class Student
    {
        public Student()
        {
            SpsaCases = new HashSet<SpsaCase>();
            Comments = new HashSet<Comment>();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public required string StudentNumber { get; set; }
        public required CPRNumber CPRNumber { get; set; }
        public required SensitiveString Name { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Guid? StartPeriodId { get; set; }
        public Period? StartPeriod { get; set; }

        public Guid? EducationalProgramId { get; set; }
        public EducationalProgram? EducationalProgram { get; set; }

        public ICollection<SpsaCase> SpsaCases { get; init; }
        public ICollection<Comment> Comments { get; init; }
    }
}