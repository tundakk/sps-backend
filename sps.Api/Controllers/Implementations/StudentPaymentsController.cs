using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// StudentPaymentsController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentPaymentsController : BaseController<StudentPaymentsController>
    {
        private readonly IStudentPaymentService _studentPaymentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentPaymentsController"/> class.
        /// </summary>
        /// <param name="studentPaymentService">The StudentPayment service.</param>
        /// <param name="logger">The logger.</param>
        public StudentPaymentsController(IStudentPaymentService studentPaymentService, ILogger<StudentPaymentsController> logger)
            : base(logger)
        {
            _studentPaymentService = studentPaymentService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _studentPaymentService.GetAllAsync();
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
                var response = await _studentPaymentService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] StudentPaymentModel studentPaymentModel)
        {
            try
            {
                var response = await _studentPaymentService.InsertAsync(studentPaymentModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the StudentPayment");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StudentPaymentModel studentPaymentModel)
        {
            try
            {
                studentPaymentModel.Id = id;
                var response = await _studentPaymentService.UpdateAsync(studentPaymentModel);
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
                var response = await _studentPaymentService.DeleteAsync(id);
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