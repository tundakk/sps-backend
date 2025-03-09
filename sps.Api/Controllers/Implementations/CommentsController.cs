using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.OpkvalSupervision;
using sps.Domain.Model.Dtos.SpsaCase;
using sps.Domain.Model.Dtos.Student;
using sps.Domain.Model.Dtos.StudentPayment;
using sps.Domain.Model.Dtos.TeacherPayment;

namespace sps.API.Controllers.Implementations
{
    public class CommentsController : ApiControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        #region SPSA Case Comments

        [HttpPost("spsacase")]
        [ProducesResponseType(typeof(SpsaCaseCommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSpsaCaseComment([FromBody] AddSpsaCaseCommentDto commentDto)
        {
            try
            {
                var result = await _commentService.AddSpsaCaseCommentAsync(commentDto);
                return CreatedAtAction(nameof(AddSpsaCaseComment), result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment to SPSA case");
            }
        }

        [HttpDelete("spsacase/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSpsaCaseComment(Guid id)
        {
            var deleted = await _commentService.DeleteSpsaCaseCommentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Student Comments

        [HttpPost("student")]
        [ProducesResponseType(typeof(StudentCommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentComment([FromBody] AddStudentCommentDto commentDto)
        {
            try
            {
                var result = await _commentService.AddStudentCommentAsync(commentDto);
                return CreatedAtAction(nameof(AddStudentComment), result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment to student");
            }
        }

        [HttpDelete("student/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudentComment(Guid id)
        {
            var deleted = await _commentService.DeleteStudentCommentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Teacher Payment Comments

        [HttpPost("teacherpayment")]
        [ProducesResponseType(typeof(TeacherPaymentCommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTeacherPaymentComment([FromBody] AddTeacherPaymentCommentDto commentDto)
        {
            try
            {
                var result = await _commentService.AddTeacherPaymentCommentAsync(commentDto);
                return CreatedAtAction(nameof(AddTeacherPaymentComment), result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment to teacher payment");
            }
        }

        [HttpDelete("teacherpayment/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeacherPaymentComment(Guid id)
        {
            var deleted = await _commentService.DeleteTeacherPaymentCommentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Student Payment Comments

        [HttpPost("studentpayment")]
        [ProducesResponseType(typeof(StudentPaymentCommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentPaymentComment([FromBody] AddStudentPaymentCommentDto commentDto)
        {
            try
            {
                var result = await _commentService.AddStudentPaymentCommentAsync(commentDto);
                return CreatedAtAction(nameof(AddStudentPaymentComment), result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment to student payment");
            }
        }

        [HttpDelete("studentpayment/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudentPaymentComment(Guid id)
        {
            var deleted = await _commentService.DeleteStudentPaymentCommentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion

        #region Opkval Supervision Comments

        [HttpPost("opkvalsupervision")]
        [ProducesResponseType(typeof(OpkvalSupervisionCommentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOpkvalSupervisionComment([FromBody] AddOpkvalSupervisionCommentDto commentDto)
        {
            try
            {
                var result = await _commentService.AddOpkvalSupervisionCommentAsync(commentDto);
                return CreatedAtAction(nameof(AddOpkvalSupervisionComment), result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding comment to supervision");
            }
        }

        [HttpDelete("opkvalsupervision/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOpkvalSupervisionComment(Guid id)
        {
            var deleted = await _commentService.DeleteOpkvalSupervisionCommentAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        #endregion
    }
}