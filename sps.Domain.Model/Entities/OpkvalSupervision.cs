using System;
using System.Collections.Generic;

namespace sps.Domain.Model.Entities
{
    public class OpkvalSupervision
    {
        public OpkvalSupervision()
        {
            SpsaCases = new HashSet<SpsaCase>();
            Comments = new HashSet<Comment>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// Date of entry / payment date / dato for tastning /betalingsdato
        /// </summary>
        public required DateTime CreateDate { get; set; } 
        
        public string? LastUpdatedBy { get; set; }
        public int? HoursSought { get; set; }
        public int QualificationHoursSpent { get; set; }
        public int SupervisionHoursSpent { get; set; }
        public OpkvalSupervisionStatus Status { get; set; }
        
        public ICollection<SpsaCase> SpsaCases { get; init; }
        public ICollection<Comment> Comments { get; init; }
    }
}