using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment.</param>
        /// <returns>The appointment details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Retrieves appointments based on local application ID and test type.
        /// </summary>
        /// <param name="localAppId">The local application ID.</param>
        /// <param name="testTypeId">The test type ID.</param>
        /// <returns>A list of matching appointments.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] int localAppId, [FromQuery] int testTypeId)
        {
            if (localAppId <= 0 || testTypeId <= 0)
            {
                return BadRequest("Local Application ID and Test Type ID are required.");
            }

            var result = await _appointmentService.GetAppointmentsByLocalAppIdAsync(localAppId, testTypeId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="dto">The appointment creation data.</param>
        /// <returns>The ID of the created appointment.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateNewAppointmentDto dto)
        {
            var result = await _appointmentService.CreateAppointmentAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Data }, result.Data);
        }

        /// <summary>
        /// Reschedules an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment.</param>
        /// <param name="dto">The new appointment details.</param>
        /// <returns>A success message.</returns>
        [HttpPut("{id}/reschedule")]
        public async Task<IActionResult> RescheduleAppointment(int id, [FromBody] UpdateAppointmentDateDto dto)
        {
            var result = await _appointmentService.RescheduleAppointmentAsync(dto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Appointment rescheduled successfully.");
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        /// <returns>A success message.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok("Appointment deleted successfully.");
        }
    }
}
