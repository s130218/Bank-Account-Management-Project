using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Service.Common;
using BankAccountAPI.DTO;
using BankAccountAPI.Factory;
using BankAccountAPI.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountAPI.Controllers
{
    [Route("v1/customer/")]
    [ApiController]
    public class AccountManageController : Controller
    {
        #region Ctor and Properties

        private readonly IAccountService _accountService;
        private readonly IAccountFactory _accountFactory;
        public AccountManageController(IAccountService accountService, IAccountFactory accountFactory)
        {
            _accountService = accountService;
            _accountFactory = accountFactory;
        }

        #endregion

        #region Method

        [Route("account")]
        [HttpGet]
        public async Task<ActionResult> GetAllCustomerAccountAsync()
        {
            var result = await _accountService.GetAllAsync().ConfigureAwait(false);
            var dto = _accountFactory.MapAndGetCustomerAccount(result.Data);
            return Ok(dto);
        }

        [Route("account")]
        [HttpPost]
        public async Task<ActionResult<ServiceResult>> AddCustomerAccountAsync(AccountDto dto)
             => Ok(await _accountFactory.MapAndAddCustomerAccount(dto).ConfigureAwait(false));


        [Route("account")]
        [Authorize(Policy = AuthorizePolicy.AdminRole)]
        [HttpPut]
        public async Task<ActionResult<ServiceResult>> UpdateCustomerAccountAsync(AccountDto dto)
        => Ok(await _accountFactory.MapAndUpdateCustomerAsync(dto).ConfigureAwait(false));


        [Route("account/{id}")]
        [Authorize(Policy = AuthorizePolicy.AdminRole)]
        [HttpPut]
        public async Task<ActionResult<ServiceResult>> UpdateCustomerAccountStatusAsync(int id)
        => Ok(await _accountService.UpdateCustomerStatusAsync(id).ConfigureAwait(false));

        #endregion
    }
}
