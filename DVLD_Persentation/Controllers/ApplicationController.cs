
using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Applications;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILocalDrivingLicenseApplicationService _localApplicationService;

        public ApplicationController(
            IApplicationService applicationService,
            ILocalDrivingLicenseApplicationService localApplicationService)
        {
            _applicationService = applicationService;
            _localApplicationService = localApplicationService;
        }

        /// <summary>
        /// Retrieves a list of all generic applications.
        /// </summary>
        /// <returns>A list of application details.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            var result = await _applicationService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves an application by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the application.</param>
        /// <returns>The application details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var result = await _applicationService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }
        
        /// <summary>
        /// Retrieves a list of all local driving license applications.
        /// </summary>
        /// <returns>A list of local driving license applications.</returns>
        [HttpGet("local-driving-license")]
        public async Task<IActionResult> GetAllLocalApplications()
        {
            var result = await _localApplicationService.GetAllApplicationsAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a local driving license application by its ID.
        /// </summary>
        /// <param name="id">The application ID.</param>
        /// <returns>The local application details.</returns>
        [HttpGet("local-driving-license/{id}")]
        public async Task<IActionResult> GetLocalApplicationById(int id)
        {
            var result = await _localApplicationService.GetLocalDrivingLicenseApplicationByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new local driving license application.
        /// </summary>
        /// <param name="dto">The application creation data.</param>
        /// <returns>The ID of the created application.</returns>
        [HttpPost("local-driving-license")]
        public async Task<IActionResult> CreateLocalApplication([FromBody] CreateNewLocalDrivingLicenseApplicationDto dto)
        {
            var result = await _localApplicationService.CreateNewApplicationAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetLocalApplicationById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Cancels a local driving license application.
        /// </summary>
        /// <param name="id">The application ID to cancel.</param>
        /// <returns>A success message.</returns>
        [HttpPut("local-driving-license/{id}/cancel")]
        public async Task<IActionResult> CancelLocalApplication(int id)
        {
           var result = await _localApplicationService.CancelApplicationAsync(id);
           if (!result.IsSuccess)
           {
               return BadRequest(result.Message);
           }
           return Ok("Application cancelled successfully.");
        }

        /// <summary>
        /// Deletes a local driving license application.
        /// </summary>
        /// <param name="id">The application ID to delete.</param>
        /// <returns>A success message.</returns>
        [HttpDelete("local-driving-license/{id}")]
        public async Task<IActionResult> DeleteLocalApplication(int id)
        {
            var result = await _localApplicationService.DeleteApplicationAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Application deleted successfully.");
        }
    }
}
