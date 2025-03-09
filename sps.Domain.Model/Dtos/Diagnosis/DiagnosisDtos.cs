using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sps.Domain.Model.Dtos.SpsaCase;

namespace sps.Domain.Model.Dtos.Diagnosis
{
    /// <summary>
    /// Basic DTO for diagnosis data
    /// </summary>
    public class DiagnosisDto
    {
        /// <summary>
        /// The unique identifier for the diagnosis
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the diagnosis
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Number of active cases with this diagnosis
        /// </summary>
        public int ActiveCaseCount { get; set; }
    }

    /// <summary>
    /// DTO for creating a new diagnosis
    /// </summary>
    public class CreateDiagnosisDto
    {
        /// <summary>
        /// The name of the diagnosis
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing diagnosis
    /// </summary>
    public class UpdateDiagnosisDto
    {
        /// <summary>
        /// The unique identifier of the diagnosis
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the diagnosis
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Detailed DTO for diagnosis including related data
    /// </summary>
    public class DiagnosisDetailDto
    {
        /// <summary>
        /// Basic diagnosis information
        /// </summary>
        public DiagnosisDto BasicInfo { get; set; }
        
        /// <summary>
        /// Cases associated with this diagnosis
        /// </summary>
        public List<SpsaCaseSummaryDto> Cases { get; set; }
        
        public DiagnosisDetailDto()
        {
            Cases = new List<SpsaCaseSummaryDto>();
        }
    }
}