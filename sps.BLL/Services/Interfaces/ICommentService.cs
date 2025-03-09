using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using sps.Domain.Model.Dtos.SpsaCase;
using sps.Domain.Model.Dtos.Student;
using sps.Domain.Model.Dtos.TeacherPayment;
using sps.Domain.Model.Dtos.StudentPayment;
using sps.Domain.Model.Dtos.OpkvalSupervision;

namespace sps.BLL.Services.Interfaces
{
    /// <summary>
    /// Interface for services that handle comments for different entity types
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Adds a comment to an existing SPSA case
        /// </summary>
        /// <param name="commentDto">The comment data</param>
        /// <returns>The newly created comment</returns>
        Task<SpsaCaseCommentDto> AddSpsaCaseCommentAsync(AddSpsaCaseCommentDto commentDto);

        /// <summary>
        /// Adds a comment to an existing student
        /// </summary>
        /// <param name="commentDto">The comment data</param>
        /// <returns>The newly created comment</returns>
        Task<StudentCommentDto> AddStudentCommentAsync(AddStudentCommentDto commentDto);

        /// <summary>
        /// Adds a comment to an existing teacher payment
        /// </summary>
        /// <param name="commentDto">The comment data</param>
        /// <returns>The newly created comment</returns>
        Task<TeacherPaymentCommentDto> AddTeacherPaymentCommentAsync(AddTeacherPaymentCommentDto commentDto);

        /// <summary>
        /// Adds a comment to an existing student payment
        /// </summary>
        /// <param name="commentDto">The comment data</param>
        /// <returns>The newly created comment</returns>
        Task<StudentPaymentCommentDto> AddStudentPaymentCommentAsync(AddStudentPaymentCommentDto commentDto);

        /// <summary>
        /// Adds a comment to an existing supervision record
        /// </summary>
        /// <param name="commentDto">The comment data</param>
        /// <returns>The newly created comment</returns>
        Task<OpkvalSupervisionCommentDto> AddOpkvalSupervisionCommentAsync(AddOpkvalSupervisionCommentDto commentDto);

        /// <summary>
        /// Deletes a comment from an SPSA case
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteSpsaCaseCommentAsync(Guid commentId);

        /// <summary>
        /// Deletes a comment from a student
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteStudentCommentAsync(Guid commentId);

        /// <summary>
        /// Deletes a comment from a teacher payment
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteTeacherPaymentCommentAsync(Guid commentId);

        /// <summary>
        /// Deletes a comment from a student payment
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteStudentPaymentCommentAsync(Guid commentId);

        /// <summary>
        /// Deletes a comment from a supervision record
        /// </summary>
        /// <param name="commentId">ID of the comment to delete</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteOpkvalSupervisionCommentAsync(Guid commentId);
    }
}