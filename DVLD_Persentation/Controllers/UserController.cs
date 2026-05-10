using DVLD_Application.Dtos.AddDtos;
using DVLD_Application.Dtos.TransfareDtos;
using DVLD_Application.Services.Interfaces.Humans.Person;
using DVLD_Application.Services.Interfaces.Humans.User;
using Microsoft.AspNetCore.Mvc;

namespace DVLD_Persentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;

        public UserController(IUserService userService, IPersonService personService)
        {
            _userService = userService;
            _personService = personService;
        }

        // GET: /User/Index
        public async Task<IActionResult> Index()
        {
            var results = await _userService.GetAllUsersAsync();
            var users = results.Select(r => r.Data).Where(d => d != null).ToList();
            return View(users);
        }

        // GET: /User/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // GET: /User/Create
        public IActionResult Create(int? Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewUserDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Verify that the specified Person exists before creating the user
            var personExistsResult = await _personService.IsPersonExistAsync(dto.Id);
            if (!personExistsResult.IsSuccess || !personExistsResult.Data)
            {
                ModelState.AddModelError(string.Empty, "Person not found. Please provide a valid Person ID.");
                return View(dto);
            }

            var result = await _userService.CreateUserAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(dto);
            }

            TempData["Success"] = "User created successfully.";
            return RedirectToAction(nameof(Details), new { id = result.Data });
        }

        // GET: /User/ChangePassword/5
        public IActionResult ChangePassword(int id)
        {
            ViewBag.UserId = id;
            return View();
        }

        // POST: /User/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = id;
                return View(dto);
            }

            var result = await _userService.ChangePasswordAsync(dto);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                ViewBag.UserId = id;
                return View(dto);
            }

            TempData["Success"] = "Password changed successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /User/EditUsername/5
        public async Task<IActionResult> EditUsername(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.UserId = id;
            return View();
        }

        // POST: /User/EditUsername/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsername(int id, string newUserName)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                ModelState.AddModelError(string.Empty, "Username is required.");
                ViewBag.UserId = id;
                return View();
            }

            var result = await _userService.UpdateUserNameAsync(id, newUserName);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                ViewBag.UserId = id;
                return View();
            }

            TempData["Success"] = "Username updated successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /User/UpdateActivation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateActivation(int id, bool isActive)
        {
            if (isActive)
            {
                var result = await _userService.ActivateUserAsync(id);
                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }
            else
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (!result.IsSuccess)
                {
                    TempData["Error"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            TempData["Success"] = $"User {(isActive ? "activated" : "deactivated")} successfully.";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /User/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(result.Data);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}