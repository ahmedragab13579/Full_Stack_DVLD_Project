using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Humans.Driver;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        private readonly ILicenseService _licenseService;
        private readonly IInternationalLicenseService _internationalLicenseService;

        public DriverController(
            IDriverService driverService,
            ILicenseService licenseService,
            IInternationalLicenseService internationalLicenseService)
        {
            _driverService = driverService;
            _licenseService = licenseService;
            _internationalLicenseService = internationalLicenseService;
        }

        /// <summary>
        /// Retrieves a list of all drivers.
        /// </summary>
        /// <returns>A list of drivers.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var result = await _driverService.GetAllDriversAsync();
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a driver by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the driver.</param>
        /// <returns>The driver details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriverById(int id)
        {
            var result = await _driverService.GetDriverByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a driver by their associated person ID.
        /// </summary>
        /// <param name="personId">The ID of the person.</param>
        /// <returns>The driver details if found.</returns>
        [HttpGet("person/{personId}")]
        public async Task<IActionResult> GetDriverByPersonId(int personId)
        {
            var result = await _driverService.GetDriverByPersonIdAsync(personId);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves a driver by their National No.
        /// </summary>
        /// <param name="nationalNo">The driver's national number.</param>
        /// <returns>The driver details if found.</returns>
        [HttpGet("national-no/{nationalNo}")]
        public async Task<IActionResult> GetDriverByNationalNo(string nationalNo)
        {
            var result = await _driverService.GetDriverByNationalNoAsync(nationalNo);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new driver.
        /// </summary>
        /// <param name="dto">The driver creation data.</param>
        /// <returns>The ID of the created driver.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateDriver([FromBody] CreateNewDriverDto dto)
        {
            var result = await _driverService.CreateDriverAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetDriverById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Retrieves all local licenses associated with a driver.
        /// </summary>
        /// <param name="id">The driver's ID.</param>
        /// <returns>A list of licenses.</returns>
        [HttpGet("{id}/licenses")]
        public async Task<IActionResult> GetDriverLicenses(int id)
        {
            var result = await _licenseService.GetLicensesByDriverIdAsync(id);
            if (!result.IsSuccess)
            {
               return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves all international licenses associated with a driver.
        /// </summary>
        /// <param name="id">The driver's ID.</param>
        /// <returns>A list of international licenses.</returns>
        [HttpGet("{id}/international-licenses")]
        public async Task<IActionResult> GetDriverInternationalLicenses(int id)
        {
            var result = await _internationalLicenseService.GetInternationalLicensesByDriverIdAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
