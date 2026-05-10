using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Licenses.Detain;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class DetainLicenseController : Controller
    {
        private readonly IDetainLicenseService _detainLicenseService;

        public DetainLicenseController(IDetainLicenseService detainLicenseService)
        {
            _detainLicenseService = detainLicenseService;
        }

        // GET: /DetainLicense/Index
        public async Task<IActionResult> Index()
        {
            var result = await _detainLicenseService.GetAllDetainedLicensesAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            var list = result.Data.Select(r => r.Data).Where(d => d != null).ToList();
            return View(list);
        }

        // GET: /DetainLicense/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _detainLicenseService.GetDetainedLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /DetainLicense/ActiveDetention?licenseId=5
        public async Task<IActionResult> ActiveDetention(int licenseId)
        {
            var result = await _detainLicenseService.GetActiveDetainedLicenseByLicenseIdAsync(licenseId);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /DetainLicense/Detain
        public IActionResult Detain(int? licenseId)
        {
            var dto = new CreateNewDetainedLicenseDto
            {
                LicenseID = licenseId ?? 0,
                FineFees = 0m
            };
            return View(dto);
        }

        // POST: /DetainLicense/Detain
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detain(CreateNewDetainedLicenseDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _detainLicenseService.DetainLicenseAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "License detained successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /DetainLicense/Release
        public IActionResult Release(int? detainId)
        {
            var dto = new CreateNewReleaseLicenseDto
            {
                DetainedLicenseID = detainId ?? 0
            };
            return View(dto);
        }

        // POST: /DetainLicense/Release
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Release(CreateNewReleaseLicenseDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var success = await _detainLicenseService.ReleaseLicenseAsync(dto);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, "Failed to release license (License not found or not detained).");
                    return View(dto);
                }

                TempData["Success"] = "License released successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }
    }
}