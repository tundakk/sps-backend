using System;
using System.Collections.Generic;

namespace sps.Domain.Model.Entities
{
    public class EducationalProgram
    {
        public EducationalProgram()
        {
            EducationPeriodRates = new HashSet<EducationPeriodRate>();
            Students = new HashSet<Student>();
            SupportingTeachers = new HashSet<SupportingTeacher>();
        }

        public Guid Id { get; set; }
        
        // From both Education and Place
        public required string Name { get; set; } // e.g. "Bioanalytiker", "Fysioterapeut (Hillerød)"
        
        // From Place
        public string? ProgramCode { get; set; } // former PlaceNumber
        public string? Alias { get; set; }
        
        // From Education
        public Guid EduCategoryId { get; set; }
        public required EduCategory EduCategory { get; set; }

        // Navigation properties
        public ICollection<EducationPeriodRate> EducationPeriodRates { get; init; }
        public ICollection<Student> Students { get; init; }
        public ICollection<SupportingTeacher> SupportingTeachers { get; init; }
    }
}