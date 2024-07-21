using AccountManage.Web.Service;
using AccountManagement.Web.Models.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _accountService.GetAllAccountAsync().ConfigureAwait(false);

            if (response.Status)
            {
                var data = JsonConvert.DeserializeObject<List<AccountDto>>(response.Data.ToString() ?? "{ }");
                return View(data.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ActionName("Register")]
        public IActionResult CreateNewAccount()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> CreateNewAccount(AccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resp = await _accountService.AddNewAccountAsync(dto).ConfigureAwait(false);

            if (resp.Status)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Failed to register account.";
                return View();
            }
        }

        [ActionName("Edit")]
        public IActionResult UpdateAccountStatus()
        {
            return View();
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> UpdateAccountStatus(int id)
        {
            var resp = await _accountService.UpdateAccountStatus(id).ConfigureAwait(false);

            if (resp.Status)
            {
                TempData["success"] = resp.Message;
                return View("Index");
            }
            else
            {
                TempData["error"] = "Update failed.";
                return View();
            }
        }
    }
}
