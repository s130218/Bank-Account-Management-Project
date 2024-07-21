using AccountManagement.Web.Models.DTO.Auth;
using AccountManagement.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountManagement.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _authService.GetAllUsersAsync();

            if (response.Status)
            {
                var data = JsonConvert.DeserializeObject<List<ApplicationUserDTO>>(response.Data.ToString() ?? "{ }");
                return View(data.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resp = await _authService.LoginAsync(dto).ConfigureAwait(false);

            if (resp.Status)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Login failed.";
                return View();
            }
        }

        public IActionResult LogOut()
        {
            ViewBag.IsLoggedIn = false;
            _authService.Logout();
            TempData["success"] = "Logged out successfully.";
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resp = await _authService.RegisterUserAsync(dto).ConfigureAwait(false);

            if (resp.Status)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Login");
            }
            else
            {
                TempData["error"] = "Registration failed.";
                return View();
            }
        }

    }
}
