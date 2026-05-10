using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Services.Interfaces.Humans.Driver;
using DVLD_Application.Services.Interfaces.Licenses.International;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class DriverController : Controller
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

        // GET: /Driver/Index
        public async Task<IActionResult> Index()
        {
            var result = await _driverService.GetAllDriversAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /Driver/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _driverService.GetDriverByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /Driver/ByPerson?Id=5
        public async Task<IActionResult> ByPerson(int Id)
        {
            var result = await _driverService.GetDriverByIdAsync(Id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        // GET: /Driver/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: /Driver/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                ModelState.AddModelError(string.Empty, "National Number is required.");
                return View();
            }

            var result = await _driverService.GetDriverByNationalNoAsync(nationalNo);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View();
            }

            return RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        // GET: /Driver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Driver/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewDriverDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _driverService.CreateDriverAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "Driver created successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /Driver/Licenses/5
        public async Task<IActionResult> Licenses(int id)
        {
            var result = await _licenseService.GetLicensesByDriverIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
            ViewBag.DriverId = id;
            return View(result.Data);
        }

        // GET: /Driver/InternationalLicenses/5
        public async Task<IActionResult> InternationalLicenses(int id)
        {
            var result = await _internationalLicenseService.GetInternationalLicensesByDriverIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Details), new { id });
            }
            ViewBag.DriverId = id;
            return View(result.Data);
        }
    }
}