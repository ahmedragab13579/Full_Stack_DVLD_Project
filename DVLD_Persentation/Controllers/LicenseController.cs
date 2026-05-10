using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using DVLD_Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class LicenseController : Controller
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

        // GET: /License/Index
        public async Task<IActionResult> Index()
        {
            var result = await _licenseService.GetAllLicensesAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /License/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Driver");
            }
            return View(result.Data);
        }

        // GET: /License/Issue
        public IActionResult Issue()
        {
            return View();
        }

        // POST: /License/Issue
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Issue(CreateNewLicenseDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _licenseService.IssueLicenseFirstTimeAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "License issued successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /License/Renew/5
        public async Task<IActionResult> Renew(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Driver");
            }
            ViewBag.LicenseId = id;
            return View();
        }

        // POST: /License/Renew/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Renew(int id, string notes)
        {
            var result = await _licenseService.RenewLicenseAsync(id, notes);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                ViewBag.LicenseId = id;
                return View();
            }

            TempData["Success"] = "License renewed successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /License/Replace/5
        public async Task<IActionResult> Replace(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Driver");
            }
            ViewBag.LicenseId = id;
            ViewBag.Reasons = Enum.GetValues(typeof(LicenseIssueReason));
            return View();
        }

        // POST: /License/Replace/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Replace(int id, LicenseIssueReason reason, string notes)
        {
            var result = await _licenseService.ReplaceLicenseAsync(id, reason, notes);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                ViewBag.LicenseId = id;
                ViewBag.Reasons = Enum.GetValues(typeof(LicenseIssueReason));
                return View();
            }

            TempData["Success"] = "License replaced successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // POST: /License/UpdateActivation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateActivation(int id, bool isActive)
        {
            if (isActive)
            {
                var result = await _licenseService.ActivateLicenseAsync(id);
                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }
            else
            {
                var result = await _licenseService.DeactivateLicenseAsync(id);
                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            TempData["Success"] = $"License {(isActive ? "activated" : "deactivated")} successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /License/InternationalDetails/5
        public async Task<IActionResult> InternationalDetails(int id)
        {
            var result = await _internationalLicenseService.GetInternationalLicenseByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Driver");
            }
            return View(result.Data);
        }

        // GET: /License/IssueInternational
        public IActionResult IssueInternational()
        {
            return View();
        }

        // POST: /License/IssueInternational
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IssueInternational(CreateNewInternationalLicenseDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _internationalLicenseService.IssueInternationalLicenseAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "International license issued successfully.";
            return RedirectToAction(nameof(InternationalDetails), new { id = result.Data });
        }

        public async Task<IActionResult> History(int Id)
        {
            var result = await _licenseService.GetLicensesByDriverIdAsync(Id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index", "Person");
            }
            ViewBag.Id = Id;
            return View(result.Data);
        }
    }
}