using Microsoft.AspNetCore.Mvc;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.SupportingTeacher;
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
                var dtoResponse = response.Adapt<ServiceResponse<IEnumerable<SupportingTeacherDto>>>();
                return ProcessResponse(dtoResponse);
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
                var dtoResponse = response.Adapt<ServiceResponse<SupportingTeacherDetailDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new supporting teacher.
        /// </summary>
        /// <param name="createDto">The teacher details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSupportingTeacherDto createDto)
        {
            try
            {
                var model = new SupportingTeacherModel
                {
                    Name = createDto.Name,
                    Email = new SensitiveString(createDto.Email),
                    PlacesId = createDto.PlacesId
                };

                var response = await _supportingTeacherService.InsertAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<SupportingTeacherDto>>();
                return CreatedResponse(dtoResponse, nameof(GetByIdAsync), 
                    new { id = dtoResponse.Data?.Id ?? Guid.Empty });
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
        /// <param name="updateDto">The updated teacher details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateSupportingTeacherDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = new SupportingTeacherModel
                {
                    Id = updateDto.Id,
                    Name = updateDto.Name,
                    Email = new SensitiveString(updateDto.Email),
                    PlacesId = updateDto.PlacesId
                };

                var response = await _supportingTeacherService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<SupportingTeacherDto>>();
                return ProcessResponse(dtoResponse);
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
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<SupportingTeacherDetailDto>();
                return Ok(detailDto.Cases);
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
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<SupportingTeacherDetailDto>();
                return Ok(detailDto.WorkloadByPeriod);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}