namespace sps.Domain.Model.Entities
{
    /// <summary>
    /// Represents the status of an OpkvalSupervision
    /// </summary>
    public enum OpkvalSupervisionStatus
    {
        /// <summary>
        /// Approved
        /// </summary>
        Godkendt,
        
        /// <summary>
        /// Student must give consent
        /// </summary>
        StuderendeGiveSamtykke,
        
        /// <summary>
        /// Awaiting STUK
        /// </summary>
        AfventerSTUK,
        
        /// <summary>
        /// Rejected by STUK
        /// </summary>
        AfslagSTUK,
        
        /// <summary>
        /// Canceled by STUK
        /// </summary>
        AnulleretSTUK
    }
}