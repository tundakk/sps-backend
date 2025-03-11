using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using sps.Domain.Model.ValueObjects;
using Mapster;
using sps.Domain.Model.Entities;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing teacher payments
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherPaymentsController : BaseController<TeacherPaymentsController>
    {
        private readonly ITeacherPaymentService _teacherPaymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherPaymentsController"/> class.
        /// </summary>
        /// <param name="teacherPaymentService">The teacher payment service.</param>
        /// <param name="logger">The logger.</param>
        public TeacherPaymentsController(ITeacherPaymentService teacherPaymentService, ILogger<TeacherPaymentsController> logger)
            : base(logger)
        {
            _teacherPaymentService = teacherPaymentService;
        }

        /// <summary>
        /// Gets all teacher payments with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _teacherPaymentService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific teacher payment.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _teacherPaymentService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new teacher payment.
        /// </summary>
        /// <param name="model">The payment details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TeacherPaymentModel model)
        {
            try
            {
                // If comment is provided, add it to the Comments collection
                if (model.Comments?.Count > 0)
                {
                    foreach (var comment in model.Comments)
                    {
                        if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                        {
                            comment.CommentText = new SensitiveString(comment.CommentText.Value);
                        }
                    }
                }

                var response = await _teacherPaymentService.InsertAsync(model);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data?.Id ?? Guid.Empty }, response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing teacher payment.
        /// </summary>
        /// <param name="id">The ID of the payment to update.</param>
        /// <param name="model">The updated payment details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TeacherPaymentModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                // If comment is provided, add it to the Comments collection
                if (model.Comments?.Count > 0)
                {
                    foreach (var comment in model.Comments)
                    {
                        if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                        {
                            comment.CommentText = new SensitiveString(comment.CommentText.Value);
                        }
                    }
                }

                var response = await _teacherPaymentService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a teacher payment.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _teacherPaymentService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all cases covered by a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _teacherPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                return Ok(payment.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets hours summary by support type for a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/hours-by-type")]
        public async Task<IActionResult> GetHoursByTypeAsync(Guid id)
        {
            try
            {
                var response = await _teacherPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                // This endpoint may need to be reimplemented based on your model structure
                return Ok(new { message = "This endpoint may need to be reimplemented based on your model structure" });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        /// <summary>
        /// Gets all comments for a specific payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsAsync(Guid id)
        {
            try
            {
                var response = await _teacherPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                return Ok(payment.Comments);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        /// <summary>
        /// Adds a comment to an existing payment.
        /// </summary>
        /// <param name="id">The ID of the payment.</param>
        /// <param name="comment">The comment to add.</param>
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddCommentAsync(Guid id, [FromBody] CommentModel comment)
        {
            try
            {
                var response = await _teacherPaymentService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var payment = response.Data;
                
                if (!string.IsNullOrEmpty(comment.CommentText?.Value))
                {
                    comment.CommentText = new SensitiveString(comment.CommentText.Value);
                }
                
                comment.EntityType = "TeacherPayment";
                comment.EntityId = id;
                comment.CreatedAt = DateTime.UtcNow;
                
                // Create a new payment with updated comments
                var comments = payment.Comments?.Select(c => c.Adapt<CommentModel>()).ToList() ?? new List<CommentModel>();
                comments.Add(comment);
                
                var updatedPayment = new TeacherPaymentModel
                {
                    Id = payment.Id,
                    Date = payment.Date,
                    Amount = payment.Amount,
                    ExternalVoucherNumber = payment.ExternalVoucherNumber,
                    SupportTypeId = payment.SupportTypeId,
                    Comments = comments.Select(c => c.Adapt<Comment>()).ToList()
                };
                
                var updateResponse = await _teacherPaymentService.UpdateAsync(updatedPayment);
                return ProcessResponse(updateResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}