using System;
using System.Collections.Generic;

namespace sps.Domain.Model.Dtos.OpkvalSupervision
{
    /// <summary>
    /// Basic DTO for supervision information
    /// </summary>
    public class OpkvalSupervisionDto
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date of supervision
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Name of the supervisor
        /// </summary>
        public string SupervisorName { get; set; }

        /// <summary>
        /// Notes about the supervision
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Outcome of the supervision
        /// </summary>
        public string Outcome { get; set; }

        /// <summary>
        /// Hours requested for qualification/supervision
        /// </summary>
        public int HoursSought { get; set; }

        /// <summary>
        /// Hours spent on qualification
        /// </summary>
        public int QualificationHoursSpent { get; set; }

        /// <summary>
        /// Hours spent on supervision
        /// </summary>
        public int SupervisionHoursSpent { get; set; }

        /// <summary>
        /// Current status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Comments about the supervision
        /// </summary>
        public List<OpkvalSupervisionCommentDto> Comments { get; set; } = new();

        /// <summary>
        /// Number of cases involved
        /// </summary>
        public int CaseCount { get; set; }
    }
}