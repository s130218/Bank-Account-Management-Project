using AccountManage.Web.Service;
using AccountManagement.Web.Models.DTO.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountManage.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAccountService _acccountService;

        public HomeController(IAccountService acccountService)
        {
            _acccountService = acccountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> GetAccount()
        {
            List<AccountDto> list = new();

            var response = await _acccountService.GetAllAccountAsync().ConfigureAwait(false);

            if (response != null && response.Status)
            {
                list = JsonConvert.DeserializeObject<List<AccountDto>>(Convert.ToString(response.Data));
            }
            return View(list);
        }

    }
}
