using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/license-classes")]
    [ApiController]
    public class LicenseClassController : ControllerBase
    {
        private readonly ILicenseClassService _licenseClassService;

        public LicenseClassController(ILicenseClassService licenseClassService)
        {
            _licenseClassService = licenseClassService;
        }

        /// <summary>
        /// Retrieves all license classes or filters by name.
        /// </summary>
        /// <param name="name">Optional name to filter by.</param>
        /// <returns>A list of license classes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllLicenseClasses([FromQuery] string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _licenseClassService.GetLicenseClassByClassNameAsync(name);
                if (!result.IsSuccess)
                {
                    return NotFound(result.Message);
                }
                return Ok(new[] { result.Data });
            }

            var results = await _licenseClassService.GetAllLicenseClassesAsync();
            if (!results.IsSuccess)
            {
                return BadRequest(results.Message);
            }
            return Ok(results.Data);
        }

        /// <summary>
        /// Retrieves a license class by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the license class.</param>
        /// <returns>The license class details.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicenseClassById(int id)
        {
            var result = await _licenseClassService.GetLicenseClassByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new license class.
        /// </summary>
        /// <param name="dto">The license class creation data.</param>
        /// <returns>The ID of the created license class.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLicenseClass([FromBody] CreateNewLicenseClassDto dto)
        {
            var result = await _licenseClassService.CreateLicenseClassAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetLicenseClassById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Updates an existing license class.
        /// </summary>
        /// <param name="id">The ID of the license class.</param>
        /// <param name="dto">The updated data.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLicenseClass(int id, [FromBody] UpdateLicenseClassDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var result = await _licenseClassService.UpdateLicenseClassAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("License Class updated successfully.");
        }

        /// <summary>
        /// Deletes a license class by its ID.
        /// </summary>
        /// <param name="id">The ID of the license class to delete.</param>
        /// <returns>A success message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenseClass(int id)
        {
            var result = await _licenseClassService.DeleteLicenseClassAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("License Class deleted successfully.");
        }
    }
}
