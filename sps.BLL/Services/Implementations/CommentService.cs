using System;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using sps.BLL.Services.Interfaces;
using sps.DAL.DataModel;
using sps.Domain.Model.Dtos.OpkvalSupervision;
using sps.Domain.Model.Dtos.SpsaCase;
using sps.Domain.Model.Dtos.Student;
using sps.Domain.Model.Dtos.StudentPayment;
using sps.Domain.Model.Dtos.TeacherPayment;
using sps.Domain.Model.Entities;
using sps.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

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
        public async Task<SpsaCaseCommentDto> AddSpsaCaseCommentAsync(AddSpsaCaseCommentDto commentDto)
        {
            // Validate that the entity exists
            var spsaCase = await _context.SpsaCases
                .FirstOrDefaultAsync(c => c.Id == commentDto.SpsaCaseId);

            if (spsaCase == null)
            {
                _logger.LogWarning("Attempted to add comment to non-existent SPSA case with ID {SpsaCaseId}", commentDto.SpsaCaseId);
                throw new ArgumentException($"SPSA case with ID {commentDto.SpsaCaseId} not found");
            }

            // Create and save the comment
            var comment = new SpsaCaseComment
            {
                CommentText = commentDto.CommentText,
                CreatedAt = DateTime.UtcNow,
                SpsaCaseId = commentDto.SpsaCaseId
            };

            _context.SpsaCaseComments.Add(comment);
            await _context.SaveChangesAsync();

            // Return the created comment
            return new SpsaCaseCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt
            };
        }

        /// <inheritdoc />
        public async Task<StudentCommentDto> AddStudentCommentAsync(AddStudentCommentDto commentDto)
        {
            // Validate that the entity exists
            var student = await _context.Students
                .FirstOrDefaultAsync(c => c.Id == commentDto.StudentId);

            if (student == null)
            {
                _logger.LogWarning("Attempted to add comment to non-existent student with ID {StudentId}", commentDto.StudentId);
                throw new ArgumentException($"Student with ID {commentDto.StudentId} not found");
            }

            // Create and save the comment
            var comment = new StudentComment
            {
                CommentText = new SensitiveString(commentDto.CommentText),
                CreatedAt = DateTime.UtcNow,
                StudentId = commentDto.StudentId
            };

            _context.StudentComments.Add(comment);
            await _context.SaveChangesAsync();

            // Return the created comment
            return new StudentCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText.Value,
                CreatedAt = comment.CreatedAt
            };
        }

        /// <inheritdoc />
        public async Task<TeacherPaymentCommentDto> AddTeacherPaymentCommentAsync(AddTeacherPaymentCommentDto commentDto)
        {
            // Validate that the entity exists
            var payment = await _context.TeacherPayments
                .FirstOrDefaultAsync(c => c.Id == commentDto.TeacherPaymentId);

            if (payment == null)
            {
                _logger.LogWarning("Attempted to add comment to non-existent teacher payment with ID {TeacherPaymentId}", commentDto.TeacherPaymentId);
                throw new ArgumentException($"Teacher payment with ID {commentDto.TeacherPaymentId} not found");
            }

            // Create and save the comment
            var comment = new TeacherPaymentComment
            {
                CommentText = new SensitiveString(commentDto.CommentText),
                CreatedAt = DateTime.UtcNow,
                TeacherPaymentId = commentDto.TeacherPaymentId
            };

            _context.TeacherPaymentComments.Add(comment);
            await _context.SaveChangesAsync();

            // Return the created comment
            return new TeacherPaymentCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText.Value,
                CreatedAt = comment.CreatedAt
            };
        }

        /// <inheritdoc />
        public async Task<StudentPaymentCommentDto> AddStudentPaymentCommentAsync(AddStudentPaymentCommentDto commentDto)
        {
            // Validate that the entity exists
            var payment = await _context.StudentPayments
                .FirstOrDefaultAsync(c => c.Id == commentDto.StudentPaymentId);

            if (payment == null)
            {
                _logger.LogWarning("Attempted to add comment to non-existent student payment with ID {StudentPaymentId}", commentDto.StudentPaymentId);
                throw new ArgumentException($"Student payment with ID {commentDto.StudentPaymentId} not found");
            }

            // Create and save the comment
            var comment = new StudentPaymentComment
            {
                CommentText = new SensitiveString(commentDto.CommentText),
                CreatedAt = DateTime.UtcNow,
                StudentPaymentId = commentDto.StudentPaymentId
            };

            _context.StudentPaymentComments.Add(comment);
            await _context.SaveChangesAsync();

            // Return the created comment
            return new StudentPaymentCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText.Value,
                CreatedAt = comment.CreatedAt
            };
        }

        /// <inheritdoc />
        public async Task<OpkvalSupervisionCommentDto> AddOpkvalSupervisionCommentAsync(AddOpkvalSupervisionCommentDto commentDto)
        {
            // Validate that the entity exists
            var supervision = await _context.OpkvalSupervisions
                .FirstOrDefaultAsync(c => c.Id == commentDto.OpkvalSupervisionId);

            if (supervision == null)
            {
                _logger.LogWarning("Attempted to add comment to non-existent supervision with ID {OpkvalSupervisionId}", commentDto.OpkvalSupervisionId);
                throw new ArgumentException($"Supervision with ID {commentDto.OpkvalSupervisionId} not found");
            }

            // Create and save the comment
            var comment = new OpkvalSupervisionComment
            {
                CommentText = new SensitiveString(commentDto.CommentText),
                CreatedAt = DateTime.UtcNow,
                OpkvalSupervisionId = commentDto.OpkvalSupervisionId
            };

            _context.OpkvalSupervisionComments.Add(comment);
            await _context.SaveChangesAsync();

            // Return the created comment
            return new OpkvalSupervisionCommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText.Value,
                CreatedAt = comment.CreatedAt
            };
        }

        /// <inheritdoc />
        public async Task<bool> DeleteSpsaCaseCommentAsync(Guid commentId)
        {
            var comment = await _context.SpsaCaseComments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogWarning("Attempted to delete non-existent SPSA case comment with ID {CommentId}", commentId);
                return false;
            }

            _context.SpsaCaseComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteStudentCommentAsync(Guid commentId)
        {
            var comment = await _context.StudentComments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogWarning("Attempted to delete non-existent student comment with ID {CommentId}", commentId);
                return false;
            }

            _context.StudentComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteTeacherPaymentCommentAsync(Guid commentId)
        {
            var comment = await _context.TeacherPaymentComments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogWarning("Attempted to delete non-existent teacher payment comment with ID {CommentId}", commentId);
                return false;
            }

            _context.TeacherPaymentComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteStudentPaymentCommentAsync(Guid commentId)
        {
            var comment = await _context.StudentPaymentComments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogWarning("Attempted to delete non-existent student payment comment with ID {CommentId}", commentId);
                return false;
            }

            _context.StudentPaymentComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteOpkvalSupervisionCommentAsync(Guid commentId)
        {
            var comment = await _context.OpkvalSupervisionComments.FindAsync(commentId);
            if (comment == null)
            {
                _logger.LogWarning("Attempted to delete non-existent supervision comment with ID {CommentId}", commentId);
                return false;
            }

            _context.OpkvalSupervisionComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}