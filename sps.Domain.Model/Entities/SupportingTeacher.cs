using System;
using System.Collections.Generic;
using sps.Domain.Model.ValueObjects;

namespace sps.Domain.Model.Entities
{
    public class SupportingTeacher
    {
        public SupportingTeacher()
        {
            SpsaCases = new HashSet<SpsaCase>();
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required SensitiveString Email { get; set; }
        
        public Guid? EducationalProgramId { get; set; }
        public EducationalProgram? EducationalProgram { get; set; }
        
        public ICollection<SpsaCase> SpsaCases { get; init; }
    }
}