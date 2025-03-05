using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// TeacherPaymentsController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherPaymentsController : BaseController<TeacherPaymentsController>
    {
        private readonly ITeacherPaymentService _teacherPaymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherPaymentsController"/> class.
        /// </summary>
        /// <param name="teacherPaymentService">The TeacherPayment service.</param>
        /// <param name="logger">The logger.</param>
        public TeacherPaymentsController(ITeacherPaymentService teacherPaymentService, ILogger<TeacherPaymentsController> logger)
            : base(logger)
        {
            _teacherPaymentService = teacherPaymentService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _teacherPaymentService.GetAllAsync();
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
                var response = await _teacherPaymentService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] TeacherPaymentModel teacherPaymentModel)
        {
            try
            {
                var response = await _teacherPaymentService.InsertAsync(teacherPaymentModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the TeacherPayment");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TeacherPaymentModel teacherPaymentModel)
        {
            try
            {
                teacherPaymentModel.Id = id;
                var response = await _teacherPaymentService.UpdateAsync(teacherPaymentModel);
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
                var response = await _teacherPaymentService.DeleteAsync(id);
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