namespace sps.Domain.Model.Dtos.SpsaCase
{
    /// <summary>
    /// A summary DTO for SPSA cases, intended for use in lists and related entities
    /// </summary>
    public class SpsaCaseSummaryDto
    {
        /// <summary>
        /// The unique identifier for the SPSA case
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The case reference number
        /// </summary>
        public string CaseNumber { get; set; }

        /// <summary>
        /// The current status of the case
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The date when the case was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date when the case was last updated
        /// </summary>
        public DateTime? LastUpdatedDate
        {
            get; set;
        }
    }
}