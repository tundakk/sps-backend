using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
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
                return ProcessResponse(response);
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
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a new education program.
        /// </summary>
        /// <param name="model">The program details.</param>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EducationModel model)
        {
            try
            {
                var response = await _educationService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetByIdAsync), 
                    new { id = response.Data?.Id ?? Guid.Empty });
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
        /// <param name="model">The updated program details.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EducationModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _educationService.UpdateAsync(model);
                return ProcessResponse(response);
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
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var education = response.Data;
                return Ok(education.EducationPeriodRates);
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
                if (!response.Success || response.Data == null)
                {
                    return ProcessResponse(response);
                }

                var education = response.Data;
                return Ok(education.Students);
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
        /// <param name="model">The rate configuration details.</param>
        [HttpPost("{id}/period-rates")]
        public async Task<IActionResult> CreatePeriodRateAsync(Guid id, [FromBody] EducationPeriodRateModel model)
        {
            try
            {
                if (id != model.EducationId)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _rateService.InsertAsync(model);
                return CreatedResponse(response, nameof(GetPeriodRatesAsync), 
                    new { id = model.EducationId });
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
        /// <param name="model">The updated rate configuration details.</param>
        [HttpPut("{id}/period-rates/{rateId}")]
        public async Task<IActionResult> UpdatePeriodRateAsync(Guid id, Guid rateId, [FromBody] EducationPeriodRateModel model)
        {
            try
            {
                if (id != model.EducationId || rateId != model.Id)
                {
                    return BadRequest("ID mismatch between URL and body");
                }

                var response = await _rateService.UpdateAsync(model);
                return ProcessResponse(response);
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