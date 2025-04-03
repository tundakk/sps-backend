using System;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.EducationalProgram
{
    /// <summary>
    /// DTO for creating a new educational program
    /// </summary>
    public class CreateEducationalProgramDto
    {
        /// <summary>
        /// The name of the educational program
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
        
        /// <summary>
        /// The official program code
        /// </summary>
        [MaxLength(50)]
        public string ProgramCode { get; set; }
        
        /// <summary>
        /// Alternative name for the program
        /// </summary>
        [MaxLength(100)]
        public string Alias { get; set; }
        
        /// <summary>
        /// The ID of the education category
        /// </summary>
        [Required(ErrorMessage = "Education category is required")]
        public Guid EduCategoryId { get; set; }
    }
}