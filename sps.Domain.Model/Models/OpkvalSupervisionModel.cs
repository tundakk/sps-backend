using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class OpkvalSupervisionModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; set; }
    }
}