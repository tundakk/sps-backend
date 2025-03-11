using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sps.BLL.Services.Interfaces;
using sps.DAL.DataModel;
using sps.Domain.Model.Entities;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using sps.Domain.Model.ValueObjects;

namespace sps.BLL.Services.Implementations
{
    /// <summary>
    /// Service that handles comments for different entity types
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly SpsDbContext _context;
        private readonly ILogger<CommentService> _logger;

        public CommentService(SpsDbContext context, ILogger<CommentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ServiceResponse<CommentModel>> AddCommentAsync(CommentModel commentModel)
        {
            try
            {
                // Validate that the entity exists based on EntityType
                if (!await EntityExistsAsync(commentModel.EntityType, commentModel.EntityId))
                {
                    _logger.LogWarning("Attempted to add comment to non-existent {EntityType} with ID {EntityId}", 
                        commentModel.EntityType, commentModel.EntityId);
                    return ServiceResponse<CommentModel>.CreateError($"{commentModel.EntityType} with ID {commentModel.EntityId} not found");
                }

                // Create and save the comment
                var comment = new Comment
                {
                    CommentText = commentModel.CommentText,
                    CreatedAt = DateTime.UtcNow,
                    EntityType = commentModel.EntityType,
                    CreatedBy = commentModel.CreatedBy
                };

                // Set the appropriate foreign key based on entity type
                SetEntityReference(comment, commentModel.EntityType, commentModel.EntityId);

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                // Return the created comment
                commentModel.Id = comment.Id;
                commentModel.CreatedAt = comment.CreatedAt;
                return ServiceResponse<CommentModel>.CreateSuccess(commentModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment");
                return ServiceResponse<CommentModel>.CreateError(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<ServiceResponse<List<CommentModel>>> GetCommentsByEntityAsync(string entityType, Guid entityId)
        {
            try
            {
                var comments = await _context.Comments
                    .Where(c => c.EntityType == entityType && GetEntityId(c, entityType) == entityId)
                    .ToListAsync();

                var commentModels = comments.Select(c => new CommentModel
                {
                    Id = c.Id,
                    CommentText = c.CommentText,
                    CreatedAt = c.CreatedAt,
                    EntityType = c.EntityType,
                    EntityId = entityId,
                    CreatedBy = c.CreatedBy,
                    // You may need to set EntityName if available
                }).ToList();

                return ServiceResponse<List<CommentModel>>.CreateSuccess(commentModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving comments for {EntityType} with ID {EntityId}", entityType, entityId);
                return ServiceResponse<List<CommentModel>>.CreateError(ex.Message);
            }
        }
        
        /// <inheritdoc />
        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(commentId);
                if (comment == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent comment with ID {CommentId}", commentId);
                    return false;
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting comment with ID {CommentId}", commentId);
                return false;
            }
        }

        // Helper methods
        private async Task<bool> EntityExistsAsync(string entityType, Guid entityId)
        {
            // Check if the referenced entity exists based on entity type
            return entityType switch
            {
                "SpsaCase" => await _context.SpsaCases.AnyAsync(s => s.Id == entityId),
                "Student" => await _context.Students.AnyAsync(s => s.Id == entityId),
                "TeacherPayment" => await _context.TeacherPayments.AnyAsync(t => t.Id == entityId),
                "StudentPayment" => await _context.StudentPayments.AnyAsync(s => s.Id == entityId),
                "OpkvalSupervision" => await _context.OpkvalSupervisions.AnyAsync(o => o.Id == entityId),
                // Add more entity types as needed
                _ => false
            };
        }

        private void SetEntityReference(Comment comment, string entityType, Guid entityId)
        {
            // Set the appropriate foreign key based on entity type
            switch (entityType)
            {
                case "SpsaCase":
                    comment.SpsaCaseId = entityId;
                    break;
                case "Student":
                    comment.StudentId = entityId;
                    break;
                case "TeacherPayment":
                    comment.TeacherPaymentId = entityId;
                    break;
                case "StudentPayment":
                    comment.StudentPaymentId = entityId;
                    break;
                case "OpkvalSupervision":
                    comment.OpkvalSupervisionId = entityId;
                    break;
                // Add more entity types as needed
                default:
                    throw new ArgumentException($"Unsupported entity type: {entityType}");
            }
        }

        private Guid? GetEntityId(Comment comment, string entityType)
        {
            // Get the appropriate ID based on entity type
            return entityType switch
            {
                "SpsaCase" => comment.SpsaCaseId,
                "Student" => comment.StudentId,
                "TeacherPayment" => comment.TeacherPaymentId,
                "StudentPayment" => comment.StudentPaymentId,
                "OpkvalSupervision" => comment.OpkvalSupervisionId,
                // Add more entity types as needed
                _ => null
            };
        }
    }
}