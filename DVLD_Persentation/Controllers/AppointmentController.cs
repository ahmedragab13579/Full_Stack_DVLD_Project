using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: /Appointment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(result.Data);
        }

        // GET: /Appointment/Index?localAppId=1&testTypeId=2
        public async Task<IActionResult> Index(int localAppId, int testTypeId)
        {
            if (localAppId <= 0 || testTypeId <= 0)
            {
                TempData["Error"] = "Local Application ID and Test Type ID are required.";
                return View(Enumerable.Empty<object>());
            }

            var result = await _appointmentService.GetAppointmentsByLocalAppIdAsync(localAppId, testTypeId);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }

            ViewBag.LocalAppId = localAppId;
            ViewBag.TestTypeId = testTypeId;
            return View(result.Data);
        }

        // GET: /Appointment/Create
        public IActionResult Create(int? localAppId, int? testTypeId)
        {
            var dto = new CreateNewAppointmentDto
            {
                LocalDrivingLicenseApplicationID = localAppId ?? 0,
                TestTypeID = testTypeId ?? 0,
                AppointmentDate = DateTime.Now.AddDays(1).Date,
                PaidFees = 15.00m // Default or dynamically fetched fee
            };
            return View(dto);
        }

        // POST: /Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _appointmentService.CreateAppointmentAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "Appointment created successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /Appointment/Reschedule/5
        public async Task<IActionResult> Reschedule(int id)
        {
            var result = await _appointmentService.GetAppointmentByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            
            var dto = new UpdateAppointmentDateDto 
            { 
                AppointmentId = id,
                NewAppointmentDate = result.Data.AppointmentDate
            };
            return View(dto);
        }

        // POST: /Appointment/Reschedule/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reschedule(int id, UpdateAppointmentDateDto dto)
        {
            if (id != dto.AppointmentId)
            {
                ModelState.AddModelError(string.Empty, "Appointment ID mismatch.");
                return View(dto);
            }

            if (!ModelState.IsValid)
                return View(dto);

            var result = await _appointmentService.RescheduleAppointmentAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "Appointment rescheduled successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Appointment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int localAppId, int testTypeId)
        {
            var result = await _appointmentService.DeleteAppointmentAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] = "Appointment deleted successfully.";
            }
            return RedirectToAction(nameof(Index), new { localAppId, testTypeId });
        }
    }
}