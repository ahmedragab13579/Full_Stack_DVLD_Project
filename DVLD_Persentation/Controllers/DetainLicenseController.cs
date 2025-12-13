using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Licenses.Detain;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/detained-licenses")]
    [ApiController]
    public class DetainLicenseController : ControllerBase
    {
        private readonly IDetainLicenseService _detainLicenseService;

        public DetainLicenseController(IDetainLicenseService detainLicenseService)
        {
            _detainLicenseService = detainLicenseService;
        }

        /// <summary>
        /// Retrieves a list of all detained licenses.
        /// </summary>
        /// <returns>A list of detained licenses.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDetainedLicenses()
        {
            var result = await _detainLicenseService.GetAllDetainedLicensesAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            var list = result.Data.Select(r => r.Data).Where(d => d != null).ToList();
            return Ok(list);
        }

        /// <summary>
        /// Retrieves a detained license by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the detention.</param>
        /// <returns>The detained license details.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetainedLicenseById(int id)
        {
            var result = await _detainLicenseService.GetDetainedLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves the active detention details for a specific license ID.
        /// </summary>
        /// <param name="licenseId">The license ID.</param>
        /// <returns>The detention details if active.</returns>
        [HttpGet("license/{licenseId}")]
        public async Task<IActionResult> GetActiveDetentionByLicenseId(int licenseId)
        {
            var result = await _detainLicenseService.GetActiveDetainedLicenseByLicenseIdAsync(licenseId);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Detains a license.
        /// </summary>
        /// <param name="dto">The detention details.</param>
        /// <returns>The ID of the detention record.</returns>
        [HttpPost]
        public async Task<IActionResult> DetainLicense([FromBody] CreateNewDetainedLicenseDto dto)
        {
            var result = await _detainLicenseService.DetainLicenseAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetDetainedLicenseById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Releases a detained license.
        /// </summary>
        /// <param name="dto">The release details.</param>
        /// <returns>A success message.</returns>
        [HttpPost("release")]
        public async Task<IActionResult> ReleaseLicense([FromBody] CreateNewReleaseLicenseDto dto)
        {
            try 
            {
                var success = await _detainLicenseService.ReleaseLicenseAsync(dto);
                if (!success)
                {
                    return BadRequest("Failed to release license (License not found or not detained).");
                }
                return Ok("License released successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
