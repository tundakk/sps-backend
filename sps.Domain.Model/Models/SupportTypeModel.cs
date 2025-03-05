using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class SupportTypeModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<SpsaCaseModel> SpsaCases { get; set; }
        public ICollection<TeacherPaymentModel> TeacherPayments { get; set; }
        public ICollection<StudentPaymentModel> StudentPayments { get; set; }
    }
}