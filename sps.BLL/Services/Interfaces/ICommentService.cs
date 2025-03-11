using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;

namespace sps.BLL.Services.Interfaces
{
    /// <summary>
    /// Service for managing comments
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds a new comment to an entity
        /// </summary>
        /// <param name="commentModel">The comment to add</param>
        /// <returns>The saved comment with generated ID</returns>
        Task<ServiceResponse<CommentModel>> AddCommentAsync(CommentModel commentModel);

        /// <summary>
        /// Gets all comments for a specific entity
        /// </summary>
        /// <param name="entityType">The type of entity</param>
        /// <param name="entityId">The ID of the entity</param>
        /// <returns>List of comments for the entity</returns>
        Task<ServiceResponse<List<CommentModel>>> GetCommentsByEntityAsync(string entityType, Guid entityId);

        /// <summary>
        /// Deletes a comment
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if successful, false if comment not found</returns>
        Task<bool> DeleteCommentAsync(Guid commentId);
        
        // Remove any entity-specific comment methods like AddOpkvalSupervisionComment
        // and use the generic AddCommentAsync method instead
    }
}