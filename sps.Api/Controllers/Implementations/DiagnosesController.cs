using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;
using Mapster;
using sps.Domain.Model.Responses;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing diagnoses
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosesController : BaseController<DiagnosesController>
    {
        private readonly IDiagnosisService _diagnosisService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosesController"/> class.
        /// </summary>
        /// <param name="diagnosisService">The diagnosis service.</param>
        /// <param name="logger">The logger.</param>
        public DiagnosesController(IDiagnosisService diagnosisService, ILogger<DiagnosesController> logger)
            : base(logger)
        {
            _diagnosisService = diagnosisService;
        }

        /// <summary>
        /// Gets all diagnoses with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _diagnosisService.GetAllAsync();
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific diagnosis.
        /// </summary>
        /// <param name="id">The ID of the diagnosis to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _diagnosisService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new diagnosis.
        /// </summary>
        /// <param name="model">The diagnosis details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DiagnosisModel model)
        {
            try
            {
                var response = await _diagnosisService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing diagnosis.
        /// </summary>
        /// <param name="id">The ID of the diagnosis to update.</param>
        /// <param name="model">The updated diagnosis details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] DiagnosisModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _diagnosisService.UpdateAsync(model);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a diagnosis.
        /// </summary>
        /// <param name="id">The ID of the diagnosis to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _diagnosisService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases associated with a specific diagnosis.
        /// </summary>
        /// <param name="id">The ID of the diagnosis.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _diagnosisService.GetByIdAsync(id);
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var diagnosis = response.Data;
                return Ok(diagnosis.SpsaCases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}