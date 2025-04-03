using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sps.API.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Dtos.EducationalProgram;
using sps.Domain.Model.Models;
using System;
using System.Threading.Tasks;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// EducationalProgramsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationalProgramsController : BaseController<EducationalProgramsController>
    {
        private readonly IEducationalProgramService _educationalProgramService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EducationalProgramsController"/> class.
        /// </summary>
        /// <param name="educationalProgramService">The EducationalProgram service.</param>
        /// <param name="logger">The logger.</param>
        public EducationalProgramsController(IEducationalProgramService educationalProgramService, ILogger<EducationalProgramsController> logger)
            : base(logger)
        {
            _educationalProgramService = educationalProgramService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _educationalProgramService.GetAllAsync();
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
                var response = await _educationalProgramService.GetByIdAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        ///<inheritdoc/>
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] CreateEducationalProgramDto programDto)
        {
            try
            {
                // Map the DTO to model
                var model = programDto.Adapt<EducationalProgramModel>();
                
                var response = await _educationalProgramService.InsertAsync(model);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateEducationalProgramDto programDto)
        {
            try
            {
                if (id != programDto.Id)
                {
                    return BadRequest("ID in URL does not match ID in request body");
                }

                // Map the DTO to model
                var model = programDto.Adapt<EducationalProgramModel>();
                
                var response = await _educationalProgramService.UpdateAsync(model);
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
                var response = await _educationalProgramService.DeleteAsync(id);
                return ProcessResponse(response);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}