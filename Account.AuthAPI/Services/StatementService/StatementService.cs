using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.Repository;
using Account.AuthAPI.Repository.UnitofWork;
using Account.AuthAPI.Service.Common;
using AccountManagement.API.Repository.BankAccountRepository;
using BankAccountAPI.Model;
using BankAccountAPI.Services.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using static AccountManagement.API.Enum.TransactionType;

namespace AccountManagement.API.Services.StatementService
{
    public class StatementService : IStatementService
    {

        #region Ctor and Prop

        private readonly IUnitOfWork _unitofWork;
        private readonly IStatementRepository _statementRepository;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatementService(IUnitOfWork unitofWork, IStatementRepository statementRepository, IAccountService accountService, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _unitofWork = unitofWork;
            _statementRepository = statementRepository;
            _accountService = accountService;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion


        public async Task<ServiceResult<List<Statement>>> GetAllStatementByAccountIdAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var customerAccount = await _accountRepository.Table.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (customerAccount == null)
            {
                return ServiceResult<List<Statement>>.Fail("Account not found");
            }
            var resp = await _statementRepository.Table.Where(x => x.AccountId == customerAccount.Id).ToListAsync();

            if (!resp.Any())
                return ServiceResult<List<Statement>>.Fail("RecordNotFound");

            return new ServiceResult<List<Statement>>(true) { Data = resp.OrderByDescending(x => x.CreatedOn).ToList() };
        }

        public async Task<ServiceResult<Statement>> AddStatementAsync(Statement entity)
        {
            if (entity == null)
                return ServiceResult<Statement>.Fail("Record not found");

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var customerAccount = await _accountRepository.Table.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (customerAccount == null)
            {
                return ServiceResult<Statement>.Fail("Account not found");
            }

            entity.AccountId = customerAccount.Id;

            if ((TransactionTypeEnum)entity.TransactionType == TransactionTypeEnum.DEPOSIT)
            {
                customerAccount.TotalAmount += (decimal)entity.Amount;
            }
            else if ((TransactionTypeEnum)entity.TransactionType == TransactionTypeEnum.WITHDRAW)
            {
                if (customerAccount.TotalAmount < (decimal)entity.Amount)
                {
                    return ServiceResult<Statement>.Fail("Insufficient funds");
                }
                customerAccount.TotalAmount -= (decimal)entity.Amount;
            }
            try
            {
                await _accountService.UpdateCustomerAccountAsync(customerAccount).ConfigureAwait(false);
                await _statementRepository.AddAsync(entity).ConfigureAwait(false);
                await _unitofWork.CommitAsync().ConfigureAwait(false);
                return ServiceResult<Statement>.Success("AddNewRecord");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
