using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.Diagnosis;
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
                var dtoResponse = response.Adapt<ServiceResponse<IEnumerable<DiagnosisDto>>>();
                return ProcessResponse(dtoResponse);
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
                var dtoResponse = response.Adapt<ServiceResponse<DiagnosisDetailDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new diagnosis.
        /// </summary>
        /// <param name="createDto">The diagnosis details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDiagnosisDto createDto)
        {
            try
            {
                var model = createDto.Adapt<DiagnosisModel>();
                var response = await _diagnosisService.InsertAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<DiagnosisDto>>();
                return CreatedResponse(dtoResponse, nameof(GetByIdAsync), 
                    new { id = dtoResponse.Data?.Id ?? Guid.Empty });
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
        /// <param name="updateDto">The updated diagnosis details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateDiagnosisDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = updateDto.Adapt<DiagnosisModel>();
                var response = await _diagnosisService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<DiagnosisDto>>();
                return ProcessResponse(dtoResponse);
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
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<DiagnosisDetailDto>();
                return Ok(detailDto.Cases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}