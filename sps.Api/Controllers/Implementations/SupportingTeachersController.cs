using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using sps.Domain.Model.ValueObjects;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing supporting teachers
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupportingTeachersController : BaseController<SupportingTeachersController>
    {
        private readonly ISupportingTeacherService _supportingTeacherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportingTeachersController"/> class.
        /// </summary>
        /// <param name="supportingTeacherService">The supporting teacher service.</param>
        /// <param name="logger">The logger.</param>
        public SupportingTeachersController(
            ISupportingTeacherService supportingTeacherService,
            ILogger<SupportingTeachersController> logger)
            : base(logger)
        {
            _supportingTeacherService = supportingTeacherService;
        }

        /// <summary>
        /// Gets all supporting teachers with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _supportingTeacherService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific supporting teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new supporting teacher.
        /// </summary>
        /// <param name="model">The teacher details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SupportingTeacherModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Email?.Value))
                {
                    model.Email = new SensitiveString(model.Email.Value);
                }

                var response = await _supportingTeacherService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing supporting teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <param name="model">The updated teacher details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SupportingTeacherModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                if (!string.IsNullOrEmpty(model.Email?.Value))
                {
                    model.Email = new SensitiveString(model.Email.Value);
                }

                var response = await _supportingTeacherService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a supporting teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases assigned to a specific teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var teacher = response.Data;
                return Ok(teacher.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets workload statistics by period for a specific teacher.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        [HttpGet("{id}/period-workload")]
        public async Task<IActionResult> GetPeriodWorkloadAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.GetByIdAsync(id);
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
    }
}