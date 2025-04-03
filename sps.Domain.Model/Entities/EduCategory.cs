using System;
using System.Collections.Generic;

namespace sps.Domain.Model.Entities
{
    public class EduCategory
    {
        public EduCategory()
        {
            EducationalPrograms = new HashSet<EducationalProgram>();
            SpsaCases = new HashSet<SpsaCase>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }   // e.g. Professionsbachelor, Erhvervsakademiuddannelse, Ungdomsuddannelse

        // Navigation - replace Educations with EducationalPrograms
        public ICollection<EducationalProgram> EducationalPrograms { get; init; }
        public ICollection<SpsaCase> SpsaCases { get; init; }
    }
}