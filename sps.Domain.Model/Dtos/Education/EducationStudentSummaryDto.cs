using System;

namespace sps.Domain.Model.Dtos.Education
{
    /// <summary>
    /// Simplified student information for use in education details
    /// </summary>
    public class EducationStudentSummaryDto
    {
        /// <summary>
        /// The unique identifier of the student
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The student's number
        /// </summary>
        public string StudentNumber { get; set; }
        
        /// <summary>
        /// The student's name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The date when the student started
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// The date when the student finished (if applicable)
        /// </summary>
        public DateTime? FinishedDate { get; set; }
    }
}