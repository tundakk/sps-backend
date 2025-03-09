using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.Education;
using sps.Domain.Model.Models;
using sps.Domain.Model.Responses;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// Controller for managing education programs
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EducationsController : BaseController<EducationsController>
    {
        private readonly IEducationService _educationService;
        private readonly IEducationPeriodRateService _rateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationsController"/> class.
        /// </summary>
        /// <param name="educationService">The education service.</param>
        /// <param name="rateService">The period rate service.</param>
        /// <param name="logger">The logger.</param>
        public EducationsController(
            IEducationService educationService, 
            IEducationPeriodRateService rateService,
            ILogger<EducationsController> logger)
            : base(logger)
        {
            _educationService = educationService;
            _rateService = rateService;
        }

        /// <summary>
        /// Gets all education programs with basic information.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _educationService.GetAllAsync();
                var dtoResponse = response.Adapt<ServiceResponse<IEnumerable<EducationDto>>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets detailed information about a specific education program.
        /// </summary>
        /// <param name="id">The ID of the program to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _educationService.GetByIdAsync(id);
                var dtoResponse = response.Adapt<ServiceResponse<EducationDetailDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new education program.
        /// </summary>
        /// <param name="createDto">The program details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEducationDto createDto)
        {
            try
            {
                var model = createDto.Adapt<EducationModel>();
                var response = await _educationService.InsertAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EducationDto>>();
                return CreatedResponse(dtoResponse, nameof(GetByIdAsync), 
                    new { id = dtoResponse.Data?.Id ?? Guid.Empty });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing education program.
        /// </summary>
        /// <param name="id">The ID of the program to update.</param>
        /// <param name="updateDto">The updated program details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEducationDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = updateDto.Adapt<EducationModel>();
                var response = await _educationService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EducationDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes an education program.
        /// </summary>
        /// <param name="id">The ID of the program to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _educationService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all period rates for a specific education program.
        /// </summary>
        /// <param name="id">The ID of the education program.</param>
        [HttpGet("{id}/period-rates")]
        public async Task<IActionResult> GetPeriodRatesAsync(Guid id)
        {
            try
            {
                var response = await _educationService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<EducationDetailDto>();
                return Ok(detailDto.PeriodRates);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Gets all students enrolled in a specific education program.
        /// </summary>
        /// <param name="id">The ID of the education program.</param>
        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudentsAsync(Guid id)
        {
            try
            {
                var response = await _educationService.GetByIdAsync(id);
                if (!response.Success)
                {
                    return ProcessResponse(response);
                }

                var detailDto = response.Data.Adapt<EducationDetailDto>();
                return Ok(detailDto.Students);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new period rate for an education program.
        /// </summary>
        /// <param name="id">The ID of the education program.</param>
        /// <param name="createDto">The rate configuration details.</param>
        [HttpPost("{id}/period-rates")]
        public async Task<IActionResult> CreatePeriodRateAsync(Guid id, [FromBody] CreatePeriodRateDto createDto)
        {
            try
            {
                if (id != createDto.EducationId)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = createDto.Adapt<EducationPeriodRateModel>();
                var response = await _rateService.InsertAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EducationPeriodRateDto>>();
                return CreatedResponse(dtoResponse, nameof(GetPeriodRatesAsync), 
                    new { id = createDto.EducationId });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Updates an existing period rate configuration.
        /// </summary>
        /// <param name="id">The ID of the education program.</param>
        /// <param name="rateId">The ID of the rate configuration to update.</param>
        /// <param name="updateDto">The updated rate configuration details.</param>
        [HttpPut("{id}/period-rates/{rateId}")]
        public async Task<IActionResult> UpdatePeriodRateAsync(Guid id, Guid rateId, [FromBody] UpdatePeriodRateDto updateDto)
        {
            try
            {
                if (id != updateDto.EducationId || rateId != updateDto.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var model = updateDto.Adapt<EducationPeriodRateModel>();
                var response = await _rateService.UpdateAsync(model);
                var dtoResponse = response.Adapt<ServiceResponse<EducationPeriodRateDto>>();
                return ProcessResponse(dtoResponse);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Deletes a period rate configuration.
        /// </summary>
        /// <param name="id">The ID of the education program.</param>
        /// <param name="rateId">The ID of the rate configuration to delete.</param>
        [HttpDelete("{id}/period-rates/{rateId}")]
        public async Task<IActionResult> DeletePeriodRateAsync(Guid id, Guid rateId)
        {
            try
            {
                var response = await _rateService.DeleteAsync(rateId);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}