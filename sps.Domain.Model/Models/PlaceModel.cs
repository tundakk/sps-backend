using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Models
{
    public class PlaceModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlaceNumber { get; set; }

        [MaxLength(100)]
        public string Alias { get; set; }

        public ICollection<SupportingTeacherModel> SupportingTeachers { get; set; }
    }
}