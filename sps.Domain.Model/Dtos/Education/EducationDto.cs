using System;

namespace sps.Domain.Model.Dtos.Education
{
    /// <summary>
    /// Basic DTO for education program data
    /// </summary>
    public class EducationDto
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the education program name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the education category name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the code of the education program
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the number of students enrolled in this education program
        /// </summary>
        public int StudentCount { get; set; }
    }
}