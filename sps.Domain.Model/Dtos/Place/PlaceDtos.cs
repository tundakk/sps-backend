using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Dtos.SupportingTeacher;

namespace sps.Domain.Model.Dtos.Place
{
    /// <summary>
    /// Basic DTO for place/department data
    /// </summary>
    public class PlaceDto
    {
        /// <summary>
        /// The unique identifier for the place
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the place
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The official place number
        /// </summary>
        public string PlaceNumber { get; set; }
        
        /// <summary>
        /// Alternative name for the place
        /// </summary>
        public string Alias { get; set; }
        
        /// <summary>
        /// Number of teachers assigned to this place
        /// </summary>
        public int TeacherCount { get; set; }
    }

    /// <summary>
    /// DTO for creating a new place
    /// </summary>
    public class CreatePlaceDto
    {
        /// <summary>
        /// The name of the place
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
        
        /// <summary>
        /// The official place number
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Place number must be between 1 and 50 characters")]
        public string PlaceNumber { get; set; }
        
        /// <summary>
        /// Alternative name for the place
        /// </summary>
        [StringLength(100, ErrorMessage = "Alias cannot exceed 100 characters")]
        public string Alias { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing place
    /// </summary>
    public class UpdatePlaceDto
    {
        /// <summary>
        /// The unique identifier of the place
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the place
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
        
        /// <summary>
        /// The official place number
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Place number must be between 1 and 50 characters")]
        public string PlaceNumber { get; set; }
        
        /// <summary>
        /// Alternative name for the place
        /// </summary>
        [StringLength(100, ErrorMessage = "Alias cannot exceed 100 characters")]
        public string Alias { get; set; }
    }

    /// <summary>
    /// Detailed DTO for place including related teachers
    /// </summary>
    public class PlaceDetailDto
    {
        /// <summary>
        /// Basic place information
        /// </summary>
        public PlaceDto BasicInfo { get; set; }
        
        /// <summary>
        /// Teachers assigned to this place
        /// </summary>
        public List<SupportingTeacherDto> Teachers { get; set; }
        
        /// <summary>
        /// Statistics about teacher case loads
        /// </summary>
        public List<TeacherWorkload> TeacherWorkloads { get; set; }
        
        public PlaceDetailDto()
        {
            Teachers = new List<SupportingTeacherDto>();
            TeacherWorkloads = new List<TeacherWorkload>();
        }
    }

    /// <summary>
    /// Workload statistics for teachers at this place
    /// </summary>
    public class TeacherWorkload
    {
        /// <summary>
        /// The name of the teacher
        /// </summary>
        public string TeacherName { get; set; }
        
        /// <summary>
        /// Number of active cases assigned
        /// </summary>
        public int ActiveCaseCount { get; set; }
        
        /// <summary>
        /// Total hours sought across all active cases
        /// </summary>
        public int TotalHoursSought { get; set; }
        
        /// <summary>
        /// Total hours spent across all active cases
        /// </summary>
        public int TotalHoursSpent { get; set; }
        
        /// <summary>
        /// Average completion rate (hours spent / hours sought)
        /// </summary>
        public double CompletionRate { get; set; }
    }
}