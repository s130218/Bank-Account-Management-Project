using AccountManagement.Web.Models.DTO.Statement;
using AccountManagement.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountManagement.Web.Controllers
{
    public class StatementController : Controller
    {
        private readonly IStatementService _statementService;
        public StatementController(IStatementService statementService)
        {
            _statementService = statementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _statementService.GetAllStatementAsync().ConfigureAwait(false);

            if (response.Status)
            {
                var data = response.Data == null ? "[]" : response.Data.ToString();
                var result = JsonConvert.DeserializeObject<List<StatementDto>>(data);
                return View(result.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [ActionName("Transaction")]
        public IActionResult CreateNewAccount()
        {
            return View();
        }

        [HttpPost, ActionName("Transaction")]
        public async Task<IActionResult> CreateNewAccount(StatementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resp = await _statementService.AddTranscationAsync(dto).ConfigureAwait(false);

            if (resp.Status)
            {
                TempData["success"] = resp.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Failed to create transaction.";
                return View();
            }
        }
    }
}
