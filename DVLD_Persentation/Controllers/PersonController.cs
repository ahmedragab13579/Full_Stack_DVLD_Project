using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.UpdateDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Services.Interfaces.Humans.Person;
using DVLD_Application.Services.Interfaces.Country;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ICountryService _countryService;

        public PersonController(IPersonService personService, ICountryService countryService)
        {
            _personService = personService;
            _countryService = countryService;
        }

        // GET: /Person/Index
        public async Task<IActionResult> Index()
        {
            var result = await _personService.GetAllPeopleAsync();
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(Enumerable.Empty<object>());
            }
            return View(result.Data);
        }

        // GET: /Person/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /Person/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: /Person/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
            {
                ModelState.AddModelError(string.Empty, "National Number is required.");
                return View();
            }

            var result = await _personService.GetPersonByNationalNoAsync(nationalNo);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View();
            }

            return View("Details", result.Data);
        }

        // GET: /Person/Create
        public async Task<IActionResult> Create()
        {
            var countriesResult = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countriesResult.IsSuccess ? countriesResult.Data : new List<CountryDto>();
            return View();
        }

        // POST: /Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewPersonDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _personService.CreatePersonAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                var countriesResult = await _countryService.GetAllCountriesAsync();
                ViewBag.Countries = countriesResult.IsSuccess ? countriesResult.Data : new List<CountryDto>();
                return View(dto);
            }

            TempData["Success"] = "Person created successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /Person/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            
            var countriesResult = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countriesResult.IsSuccess ? countriesResult.Data : new List<CountryDto>();
            
            return View(result.Data);
        }

        // POST: /Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdatePersonDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _personService.UpdatePersonAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                var countriesResult = await _countryService.GetAllCountriesAsync();
                ViewBag.Countries = countriesResult.IsSuccess ? countriesResult.Data : new List<CountryDto>();
                return View(dto);
            }

            TempData["Success"] = "Person updated successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Person/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personService.GetPersonByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // POST: /Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _personService.DeletePersonAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "Person deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}