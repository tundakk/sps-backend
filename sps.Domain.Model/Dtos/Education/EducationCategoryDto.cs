using System;

namespace sps.Domain.Model.Dtos.Education
{
    /// <summary>
    /// Simple DTO for education category reference
    /// </summary>
    public class EducationCategoryDto
    {
        /// <summary>
        /// The unique identifier for the category
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; set; }
    }
}