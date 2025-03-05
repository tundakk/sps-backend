using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// EduCategorysController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EduCategorysController : BaseController<EduCategorysController>
    {
        private readonly IEduCategoryService _eduCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EduCategorysController"/> class.
        /// </summary>
        /// <param name="eduCategoryService">The EduCategory service.</param>
        /// <param name="logger">The logger.</param>
        public EduCategorysController(IEduCategoryService eduCategoryService, ILogger<EduCategorysController> logger)
            : base(logger)
        {
            _eduCategoryService = eduCategoryService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _eduCategoryService.GetAllAsync();
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
                var response = await _eduCategoryService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] EduCategoryModel eduCategoryModel)
        {
            try
            {
                var response = await _eduCategoryService.InsertAsync(eduCategoryModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the EduCategory");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EduCategoryModel eduCategoryModel)
        {
            try
            {
                eduCategoryModel.Id = id;
                var response = await _eduCategoryService.UpdateAsync(eduCategoryModel);
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
                var response = await _eduCategoryService.DeleteAsync(id);
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