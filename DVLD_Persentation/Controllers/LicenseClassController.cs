using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Licenses.Local;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class LicenseClassController : Controller
    {
        private readonly ILicenseClassService _licenseClassService;

        public LicenseClassController(ILicenseClassService licenseClassService)
        {
            _licenseClassService = licenseClassService;
        }

        // GET: /LicenseClass/Index  OR  /LicenseClass/Index?name=Car
        public async Task<IActionResult> Index(string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var single = await _licenseClassService.GetLicenseClassByClassNameAsync(name);
                if (!single.IsSuccess)
                {
                    TempData["Error"] = single.Message;
                    return View(Enumerable.Empty<object>());
                }
                ViewBag.FilterName = name;
                return View(new[] { single.Data });
            }

            var result = await _licenseClassService.GetAllLicenseClassesAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /LicenseClass/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _licenseClassService.GetLicenseClassByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /LicenseClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /LicenseClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewLicenseClassDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _licenseClassService.CreateLicenseClassAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "License Class created successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /LicenseClass/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _licenseClassService.GetLicenseClassByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            // Map to UpdateDto - adjust property names to match your actual DTO
            var dto = new UpdateLicenseClassDto { Id = id };
            return View(dto);
        }

        // POST: /LicenseClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateLicenseClassDto dto)
        {
            if (id != dto.Id)
            {
                ModelState.AddModelError(string.Empty, "ID mismatch.");
                return View(dto);
            }

            if (!ModelState.IsValid)
                return View(dto);

            var result = await _licenseClassService.UpdateLicenseClassAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "License Class updated successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /LicenseClass/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _licenseClassService.GetLicenseClassByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // POST: /LicenseClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _licenseClassService.DeleteLicenseClassAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "License Class deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}