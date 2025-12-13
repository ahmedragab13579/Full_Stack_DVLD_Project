using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using DVLD_Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/licenses")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;
        private readonly IInternationalLicenseService _internationalLicenseService;

        public LicenseController(
            ILicenseService licenseService,
            IInternationalLicenseService internationalLicenseService)
        {
            _licenseService = licenseService;
            _internationalLicenseService = internationalLicenseService;
        }

        /// <summary>
        /// Retrieves a license by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the license.</param>
        /// <returns>The license details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicenseById(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Issues a new license for the first time.
        /// </summary>
        /// <param name="dto">The license issuance data.</param>
        /// <returns>The ID of the newly issued license.</returns>
        [HttpPost]
        public async Task<IActionResult> IssueLicense([FromBody] CreateNewLicenseDto dto)
        {
            var result = await _licenseService.IssueLicenseFirstTimeAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetLicenseById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Renews an existing license.
        /// </summary>
        /// <param name="id">The ID of the license to renew.</param>
        /// <param name="notes">Optional notes for the renewal.</param>
        /// <returns>The new license ID.</returns>
        [HttpPut("{id}/renew")]
        public async Task<IActionResult> RenewLicense(int id, [FromBody] string notes)
        {
            var result = await _licenseService.RenewLicenseAsync(id, notes);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { NewLicenseId = result.Data, Message = "License renewed successfully." });
        }

        /// <summary>
        /// Replaces a lost or damaged license.
        /// </summary>
        /// <param name="id">The ID of the license to replace.</param>
        /// <param name="request">The replacement reason and notes.</param>
        /// <returns>The new license ID.</returns>
        [HttpPut("{id}/replace")]
        public async Task<IActionResult> ReplaceLicense(int id, [FromBody] ReplaceLicenseRequest request)
        {
            var result = await _licenseService.ReplaceLicenseAsync(id, request.Reason, request.Notes);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { NewLicenseId = result.Data, Message = "License replaced successfully." });
        }

        /// <summary>
        /// Activates or Deactivates a license.
        /// </summary>
        /// <param name="id">The ID of the license.</param>
        /// <param name="isActive">The new activation status.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/activation")]
        public async Task<IActionResult> UpdateActivation(int id, [FromBody] bool isActive)
        {
            if (isActive)
            {
                var result = await _licenseService.ActivateLicenseAsync(id);
                if (!result.IsSuccess) return BadRequest(result.Message);
            }
            else
            {
                var result = await _licenseService.DeactivateLicenseAsync(id);
                if (!result.IsSuccess) return BadRequest(result.Message);
            }
            return Ok($"License {(isActive ? "activated" : "deactivated")} successfully.");
        }


        /// <summary>
        /// Retrieves an international license by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the international license.</param>
        /// <returns>The international license details.</returns>
        [HttpGet("international/{id}")]
        public async Task<IActionResult> GetInternationalLicenseById(int id)
        {
            var result = await _internationalLicenseService.GetInternationalLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Issues a new international license.
        /// </summary>
        /// <param name="dto">The international license issuance data.</param>
        /// <returns>The ID of the newly issued international license.</returns>
        [HttpPost("international")]
        public async Task<IActionResult> IssueInternationalLicense([FromBody] CreateNewInternationalLicenseDto dto)
        {
            var result = await _internationalLicenseService.IssueInternationalLicenseAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetInternationalLicenseById), new { id = result.Data }, result.Data);
        }
    }

    public class ReplaceLicenseRequest
    {
        public LicenseIssueReason Reason { get; set; }
        public string Notes { get; set; }
    }
}
