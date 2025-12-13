using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Tests;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// Retrieves a test by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the test.</param>
        /// <returns>The test details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var dto = await _testService.GetTestByIdAsync(id);
            if (dto == null)
            {
                return NotFound("Test not found.");
            }
            return Ok(dto);
        }

        /// <summary>
        /// Creates a new test appointment.
        /// </summary>
        /// <param name="dto">The test creation data.</param>
        /// <returns>The ID of the scheduled test.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] CreateNewTestDto dto)
        {
            try
            {
                var testId = await _testService.CreateTestAsync(dto);
                return CreatedAtAction(nameof(GetTestById), new { id = testId }, testId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the result of a test.
        /// </summary>
        /// <param name="id">The ID of the test.</param>
        /// <param name="dto">The result update data.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/result")]
        public async Task<IActionResult> UpdateTestResult(int id, [FromBody] UpdateTestResultDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch.");
            }
            
            try
            {
                var success = await _testService.TakeTestAsync(dto);
                if (!success)
                {
                    return BadRequest("Failed to update test result (Test not found or error).");
                }
                return Ok("Test result updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
