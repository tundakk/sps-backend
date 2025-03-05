using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// StudentsController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : BaseController<StudentsController>
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentsController"/> class.
        /// </summary>
        /// <param name="studentService">The Student service.</param>
        /// <param name="logger">The logger.</param>
        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
            : base(logger)
        {
            _studentService = studentService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _studentService.GetAllAsync();
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
                var response = await _studentService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] StudentModel studentModel)
        {
            try
            {
                var response = await _studentService.InsertAsync(studentModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the Student");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StudentModel studentModel)
        {
            try
            {
                studentModel.Id = id;
                var response = await _studentService.UpdateAsync(studentModel);
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
                var response = await _studentService.DeleteAsync(id);
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