using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Service.Common;
using AccountManagement.API.Factory;
using AccountManagement.API.Services.StatementService;
using BankAccountAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.API.Controllers
{
    [Route("v1/customer/")]
    [ApiController]
    public class StatementController : Controller
    {
        private readonly IStatementService _statementService;
        private readonly IStatementFactory _statementFactory;
        public StatementController(IStatementService statementService, IStatementFactory statementFactory)
        {
            _statementService = statementService;
            _statementFactory = statementFactory;
        }


        [Route("statement")]
        [HttpGet]
        public async Task<ActionResult> GetAllStatementAsync()
        {
            var result = await _statementService.GetAllStatementByAccountIdAsync().ConfigureAwait(false);
            var dto = _statementFactory.MapAndGetCustomerStatement(result.Data);
            return Ok(dto);
        }


        [Route("statement")]
        [HttpPost]
        public async Task<ActionResult<ServiceResult>> DepositOrWithdrawAsync(StatementDto dto)
         => Ok(await _statementFactory.MapAndAddStatement(dto).ConfigureAwait(false));
    }
}
