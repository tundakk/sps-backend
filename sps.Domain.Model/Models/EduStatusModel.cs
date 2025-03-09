using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class EduStatusModel
    {
        public EduStatusModel()
        {
            SpsaCases = new HashSet<SpsaCaseModel>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        public ICollection<SpsaCaseModel> SpsaCases { get; init; }
    }
}