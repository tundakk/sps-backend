using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using sps.Domain.Model.ValueObjects;

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
                return ProcessResponse(response);
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
                return ProcessResponse(response);
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
                return CreatedResponse(response, nameof(GetByIdAsync),
                    new { id = response.Data?.Id ?? Guid.Empty });
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
                return ProcessResponse(response);
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
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpGet("cpr/{cprNumber}")]
        public async Task<IActionResult> GetByCprAsync(string cprNumber)
        {
            try
            {
                // Validate CPR number format
                if (!CPRNumber.IsValid(cprNumber))
                {
                    var validationErrors = new Dictionary<string, List<string>>
                    {
                        { nameof(cprNumber), new List<string> { "Invalid CPR number format." } }
                    };
                    var errorResponse = ServiceResponse<StudentModel>.CreateValidationError(validationErrors);
                    return BadRequest(errorResponse);
                }

                // Create CPRNumber value object
                var cpr = new CPRNumber(cprNumber);

                // Call the service method
                var response = await _studentService.GetByCprAsync(cpr);

                // Process and return the response using the ServiceResponse layer
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                return HandleError(ex);
            }
        }
    }
}