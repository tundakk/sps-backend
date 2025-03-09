using System;
using System.Collections.Generic;
using sps.Domain.Model.Dtos.SpsaCase;

namespace sps.Domain.Model.Dtos.Student
{
    /// <summary>
    /// DTO for detailed student information including related entities
    /// </summary>
    public class StudentDetailDto
    {
        /// <summary>
        /// The unique identifier for the student
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The student's academic identification number
        /// </summary>
        public string StudentNumber { get; set; }
        
        /// <summary>
        /// The student's name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Optional comments about the student
        /// </summary>
        public string Comment { get; set; }
        
        /// <summary>
        /// The date when the student completed their education (if applicable)
        /// </summary>
        public DateTime? FinishedDate { get; set; }
        
        /// <summary>
        /// Information about the student's starting period
        /// </summary>
        public PeriodDto StartPeriod { get; set; }
        
        /// <summary>
        /// Information about the student's education program
        /// </summary>
        public EducationDto Education { get; set; }
        
        /// <summary>
        /// SPSA cases associated with the student
        /// </summary>
        public List<SpsaCaseSummaryDto> SpsaCases { get; set; }
        
        /// <summary>
        /// Initialize collections
        /// </summary>
        public StudentDetailDto()
        {
            SpsaCases = new List<SpsaCaseSummaryDto>();
        }
    }
    
    /// <summary>
    /// Simplified DTO for period information used within student details
    /// </summary>
    public class PeriodDto
    {
        /// <summary>
        /// The unique identifier of the period
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the period
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The start date of the period
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// The end date of the period
        /// </summary>
        public DateTime EndDate { get; set; }
    }
    
    /// <summary>
    /// Simplified DTO for education information used within student details
    /// </summary>
    public class EducationDto
    {
        /// <summary>
        /// The unique identifier of the education program
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the education program
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The category of the education program
        /// </summary>
        public string CategoryName { get; set; }
    }
}