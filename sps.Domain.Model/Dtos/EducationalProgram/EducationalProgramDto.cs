using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sps.Domain.Model.Dtos.EducationalProgram
{
    /// <summary>
    /// Basic DTO for educational program data
    /// </summary>
    public class EducationalProgramDto
    {
        /// <summary>
        /// The unique identifier for the educational program
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the educational program
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The official program code
        /// </summary>
        public string ProgramCode { get; set; }
        
        /// <summary>
        /// Alternative name for the program
        /// </summary>
        public string Alias { get; set; }
        
        /// <summary>
        /// The category of the educational program
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Number of students enrolled in this program
        /// </summary>
        public int StudentCount { get; set; }
        
        /// <summary>
        /// Number of teachers assigned to this program
        /// </summary>
        public int TeacherCount { get; set; }
    }
}