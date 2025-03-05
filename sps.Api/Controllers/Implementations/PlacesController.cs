using Microsoft.AspNetCore.Mvc;
using sps.Api.Controllers.Base;
using sps.BLL.Services.Interfaces;
using sps.Domain.Model.Models;

namespace sps.API.Controllers.Implementations
{
    /// <summary>
    /// PlacesController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : BaseController<PlacesController>
    {
        private readonly IPlaceService _placeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlacesController"/> class.
        /// </summary>
        /// <param name="placeService">The Place service.</param>
        /// <param name="logger">The logger.</param>
        public PlacesController(IPlaceService placeService, ILogger<PlacesController> logger)
            : base(logger)
        {
            _placeService = placeService;
        }

        ///<inheritdoc/>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _placeService.GetAllAsync();
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
                var response = await _placeService.GetByIdAsync(id);
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
        public async Task<IActionResult> InsertAsync([FromBody] PlaceModel placeModel)
        {
            try
            {
                var response = await _placeService.InsertAsync(placeModel);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                if (response.Data == null)
                {
                    return BadRequest("Failed to create the Place");
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] PlaceModel placeModel)
        {
            try
            {
                placeModel.Id = id;
                var response = await _placeService.UpdateAsync(placeModel);
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
                var response = await _placeService.DeleteAsync(id);
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