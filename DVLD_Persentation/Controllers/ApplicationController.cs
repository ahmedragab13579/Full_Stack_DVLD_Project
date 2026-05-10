using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Applications;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly ILocalDrivingLicenseApplicationService _localApplicationService;
        private readonly ILicenseClassService _licenseClassService;

        public ApplicationController(
            IApplicationService applicationService,
            ILocalDrivingLicenseApplicationService localApplicationService,
            ILicenseClassService licenseClassService)
        {
            _applicationService = applicationService;
            _localApplicationService = localApplicationService;
            _licenseClassService = licenseClassService;
        }

        // GET: /Application/Index
        public async Task<IActionResult> Index()
        {
            var result = await _applicationService.GetAllAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /Application/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _applicationService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /Application/LocalIndex
        public async Task<IActionResult> LocalIndex()
        {
            var result = await _localApplicationService.GetAllApplicationsAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /Application/LocalDetails/5
        public async Task<IActionResult> LocalDetails(int id)
        {
            var result = await _localApplicationService.GetLocalDrivingLicenseApplicationByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(LocalIndex));
            }
            return View(result.Data);
        }

        // GET: /Application/Create
        public async Task<IActionResult> Create()
        {
            var licenseClassesResult = await _licenseClassService.GetAllLicenseClassesAsync();
            ViewBag.LicenseClasses = licenseClassesResult.IsSuccess ? licenseClassesResult.Data : Enumerable.Empty<object>();
            return View();
        }

        // POST: /Application/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewLocalDrivingLicenseApplicationDto dto)
        {
            // ApplicationID is auto-generated, so we shouldn't validate it on creation
            ModelState.Remove(nameof(dto.ApplicationID));

            if (!ModelState.IsValid)
            {
                var licenseClassesResult = await _licenseClassService.GetAllLicenseClassesAsync();
                ViewBag.LicenseClasses = licenseClassesResult.IsSuccess ? licenseClassesResult.Data : Enumerable.Empty<object>();
                return View(dto);
            }

            var result = await _localApplicationService.CreateNewApplicationAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                var licenseClassesResult = await _licenseClassService.GetAllLicenseClassesAsync();
                ViewBag.LicenseClasses = licenseClassesResult.IsSuccess ? licenseClassesResult.Data : Enumerable.Empty<object>();
                return View(dto);
            }

            TempData["Success"] = "Application created successfully.";
            return RedirectToAction(nameof(LocalDetails), new { id = result.Data });
        }

        // POST: /Application/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _localApplicationService.CancelApplicationAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] = "Application cancelled successfully.";
            }
            return RedirectToAction(nameof(LocalDetails), new { id });
        }

        // POST: /Application/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _localApplicationService.DeleteApplicationAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(LocalDetails), new { id });
            }

            TempData["Success"] = "Application deleted successfully.";
            return RedirectToAction(nameof(LocalIndex));
        }
    }
}