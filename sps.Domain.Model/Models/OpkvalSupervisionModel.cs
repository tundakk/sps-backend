using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Entities;

namespace sps.Domain.Model.Models
{
    public class OpkvalSupervisionModel
    {
        public OpkvalSupervisionModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
            Comments = new HashSet<Comment>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string SupervisorName { get; set; }
        public string Notes { get; set; }
        public string Outcome { get; set; }
        public int HoursSought { get; set; }
        public int QualificationHoursSpent { get; set; }
        public int SupervisionHoursSpent { get; set; }
        public string Status { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; set; }
        public ICollection<Comment> Comments { get; init; }
    }
}