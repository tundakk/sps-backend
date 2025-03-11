using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing SPSA (Special Pedagogical Support Administration) cases
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SpsaCasesController : BaseController<SpsaCasesController>
    {
        private readonly ISpsaCaseService _spsaCaseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpsaCasesController"/> class.
        /// </summary>
        /// <param name="spsaCaseService">The SpsaCase service.</param>
        /// <param name="logger">The logger.</param>
        public SpsaCasesController(ISpsaCaseService spsaCaseService, ILogger<SpsaCasesController> logger)
            : base(logger)
        {
            _spsaCaseService = spsaCaseService;
        }

        /// <summary>
        /// Gets all SPSA cases with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _spsaCaseService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific SPSA case.
        /// </summary>
        /// <param name="id">The ID of the SPSA case to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _spsaCaseService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new SPSA case.
        /// </summary>
        /// <param name="model">The SPSA case details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SpsaCaseModel model)
        {
            try
            {
                var response = await _spsaCaseService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing SPSA case.
        /// </summary>
        /// <param name="id">The ID of the SPSA case to update.</param>
        /// <param name="model">The updated SPSA case details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SpsaCaseModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _spsaCaseService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a SPSA case.
        /// </summary>
        /// <param name="id">The ID of the SPSA case to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _spsaCaseService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases for a specific student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentIdAsync(Guid studentId)
        {
            try
            {
                var response = await _spsaCaseService.GetAllAsync();
                // Filter the results to only include cases for this student
                if (response.Success)
                {
                    var filteredData = response.Data?.Where(c => c.StudentId == studentId).ToList() ?? new List<SpsaCaseModel>();
                    response = ServiceResponse<IEnumerable<SpsaCaseModel>>.CreateSuccess(filteredData);
                }
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}