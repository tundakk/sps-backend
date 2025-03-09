using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.EduStatus;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using Mapster;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing education statuses
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EduStatusesController : BaseController<EduStatusesController>
    {
        private readonly IEduStatusService _eduStatusService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EduStatusesController"/> class.
        /// </summary>
        /// <param name="eduStatusService">The education status service.</param>
        /// <param name="logger">The logger.</param>
        public EduStatusesController(IEduStatusService eduStatusService, ILogger<EduStatusesController> logger)
            : base(logger)
        {
            _eduStatusService = eduStatusService;
        }

        /// <summary>
        /// Gets all education statuses with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _eduStatusService.GetAllAsync();
                var dtoResponse = response.Adapt<ServiceResponse<IEnumerable<EduStatusDto>>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific education status.
        /// </summary>
        /// <param name="id">The ID of the status to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _eduStatusService.GetByIdAsync(id);
                var dtoResponse = response.Adapt<ServiceResponse<EduStatusDetailDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new education status.
        /// </summary>
        /// <param name="createDto">The status details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEduStatusDto createDto)
        {
            try
            {
                var model = createDto.Adapt<EduStatusModel>();
                var response = await _eduStatusService.InsertAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EduStatusDto>>();
                return CreatedResponse(dtoResponse, nameof(GetByIdAsync), 
                    new { id = dtoResponse.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing education status.
        /// </summary>
        /// <param name="id">The ID of the status to update.</param>
        /// <param name="updateDto">The updated status details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEduStatusDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = updateDto.Adapt<EduStatusModel>();
                var response = await _eduStatusService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EduStatusDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes an education status.
        /// </summary>
        /// <param name="id">The ID of the status to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _eduStatusService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all SPSA cases with a specific education status.
        /// </summary>
        /// <param name="id">The ID of the status.</param>
        [HttpGet("{id}/cases")]
        public async Task<IActionResult> GetCasesAsync(Guid id)
        {
            try
            {
                var response = await _eduStatusService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<EduStatusDetailDto>();
                return Ok(detailDto.Cases);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets statistics by education category for a specific status.
        /// </summary>
        /// <param name="id">The ID of the status.</param>
        [HttpGet("{id}/category-stats")]
        public async Task<IActionResult> GetCategoryStatsAsync(Guid id)
        {
            try
            {
                var response = await _eduStatusService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<EduStatusDetailDto>();
                return Ok(detailDto.StatsByCategory);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}