using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.TeacherPayment;

using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using sps.Domain.Model.ValueObjects;
using Mapster;

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
                var dtoResponse = response.Adapt<ServiceResponse<IEnumerable<TeacherPaymentDto>>>();
                return ProcessResponse(dtoResponse);
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
                var dtoResponse = response.Adapt<ServiceResponse<TeacherPaymentDetailDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new teacher payment.
        /// </summary>
        /// <param name="createDto">The payment details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTeacherPaymentDto createDto)
        {
            try
            {
                var model = createDto.Adapt<TeacherPaymentModel>();
                if (!string.IsNullOrEmpty(createDto.Comment))
                {
                    model.Comment = new SensitiveString(createDto.Comment);
                }

                var response = await _teacherPaymentService.InsertAsync(model);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var dtoResponse = response.Adapt<ServiceResponse<TeacherPaymentDetailDto>>();
                return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data?.Id ?? Guid.Empty }, dtoResponse.Data);
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
        /// <param name="updateDto">The updated payment details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTeacherPaymentDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                var model = updateDto.Adapt<TeacherPaymentModel>();
                if (!string.IsNullOrEmpty(updateDto.Comment))
                {
                    model.Comment = new SensitiveString(updateDto.Comment);
                }

                var response = await _teacherPaymentService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<TeacherPaymentDetailDto>>();
                return ProcessResponse(dtoResponse);
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
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<TeacherPaymentDetailDto>();
                return Ok(detailDto.Cases);
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
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<TeacherPaymentDetailDto>();
                return Ok(detailDto.HoursByType);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}