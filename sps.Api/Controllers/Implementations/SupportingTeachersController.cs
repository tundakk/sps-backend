using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// SupportingTeachersController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SupportingTeachersController : BaseController<SupportingTeachersController>
    {
        private readonly ISupportingTeacherService _supportingTeacherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportingTeachersController"/> class.
        /// </summary>
        /// <param name="supportingTeacherService">The SupportingTeacher service.</param>
        /// <param name="logger">The logger.</param>
        public SupportingTeachersController(ISupportingTeacherService supportingTeacherService, ILogger<SupportingTeachersController> logger)
            : base(logger)
        {
            _supportingTeacherService = supportingTeacherService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _supportingTeacherService.GetAllAsync();
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return NotFound(response.Message); // Use NotFound for invalid IDs
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] SupportingTeacherModel supportingTeacherModel)
        {
            try
            {
                var response = await _supportingTeacherService.InsertAsync(supportingTeacherModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the SupportingTeacher");
                }
                return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data.Id }, response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SupportingTeacherModel supportingTeacherModel)
        {
            try
            {
                supportingTeacherModel.Id = id;
                var response = await _supportingTeacherService.UpdateAsync(supportingTeacherModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _supportingTeacherService.DeleteAsync(id);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message); // Return a success message or confirmation
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}