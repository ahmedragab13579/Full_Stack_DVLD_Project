using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Services.Interfaces.Tests;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        // GET: /Test/Index
        public async Task<IActionResult> Index()
        {
            var list = await _testService.GetAllTestsAsync();
            return View(list);
        }

        // GET: /Test/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _testService.GetTestByIdAsync(id);
            if (dto == null)
            {
                TempData["Error"] = "Test not found.";
                return RedirectToAction("Index", "Home");
            }
            return View(dto);
        }

        // GET: /Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewTestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var testId = await _testService.CreateTestAsync(dto);
                TempData["Success"] = "Test scheduled successfully.";
                return RedirectToAction(nameof(Details), new { id = testId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        // GET: /Test/UpdateResult/5
        public async Task<IActionResult> UpdateResult(int id)
        {
            var dto = await _testService.GetTestByIdAsync(id);
            if (dto == null)
            {
                TempData["Error"] = "Test not found.";
                return RedirectToAction("Index", "Home");
            }
            // Pre-populate the update DTO with the test id
            var updateDto = new UpdateTestResultDto { Id = id };
            return View(updateDto);
        }

        // POST: /Test/UpdateResult/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateResult(int id, UpdateTestResultDto dto)
        {
            if (id != dto.Id)
            {
                ModelState.AddModelError(string.Empty, "ID mismatch.");
                return View(dto);
            }

            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var success = await _testService.TakeTestAsync(dto);
                if (!success)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update test result (Test not found or error).");
                    return View(dto);
                }

                TempData["Success"] = "Test result updated successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }
    }
}