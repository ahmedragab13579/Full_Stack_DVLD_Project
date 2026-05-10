using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Application.Services.Interfaces.Humans.Person;
using DVLD_Application.Dtos.AddDtos;

namespace DVLD_Persentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPersonService _personService;

        public HomeController(IUserService userService, IPersonService personService)
        {
            _userService = userService;
            _personService = personService;
        }

        // عرض الصفحة الرئيسية (Dashboard)
        public IActionResult Index()
        {
            // التحقق إذا كان المستخدم مسجل دخول أم لا قبل عرض اللوحة
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        // صفحة عرض الأخطاء
        public IActionResult Error()
        {
            return View();
        }

        // صفحة تسجيل الدخول (GET)
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string password, bool remember)
        {
            // 1. التحقق الأساسي من المدخلات
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            // 2. تحقق عبر طبقة الخدمات
            var result = await _userService.AuthenticateAsync(userName, password);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Invalid username or password.");
                return View();
            }

            var userDto = result.Data;

            // إعداد Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userDto.UserName ?? userName),
                // role could be extended later
                new Claim(ClaimTypes.Role, userDto.IsActive ? "User" : "Guest")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = remember,
                ExpiresUtc = remember ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["Success"] = $"Welcome back, {userDto.UserName}!";
            return RedirectToAction(nameof(Index));
        }

        // تسجيل الخروج
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction(nameof(Login));
        }

        // صفحة التسجيل (GET)
        public IActionResult Register()
        {
            return View(new CreateNewPersonDto { DateOfBirth = DateTime.Now.AddYears(-18) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateNewPersonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            // Check if person already exists by National No
            var personExistsResult = await _personService.IsPersonExistAsync(dto.NationalNo);
            if (personExistsResult.IsSuccess && personExistsResult.Data)
            {
                ModelState.AddModelError(string.Empty, "A person with this National Number already exists.");
                return View(dto);
            }

            var createResult = await _personService.CreatePersonAsync(dto);
            if (!createResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, createResult.Message);
                return View(dto);
            }

            TempData["Success"] = "Person profile created successfully.";
            return RedirectToAction(nameof(Login));
        }

        // صفحة نسيت كلمة المرور
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}