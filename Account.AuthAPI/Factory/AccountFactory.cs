using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Repository.Repository;
using Account.AuthAPI.Service.Common;
using BankAccountAPI.DTO;
using BankAccountAPI.Services.Service;
using System.Collections.Generic;
using System.Security.Claims;

namespace BankAccountAPI.Factory
{
    public class AccountFactory : IAccountFactory
    {
        #region Ctor and Prop

        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// 
        /// </summary>
        public AccountFactory(IAccountService accountService, IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        #endregion


        #region Method
        public async Task<ServiceResult<CustomerAccount>> MapAndAddCustomerAccount(AccountDto dto)
        {
            var mappedData = MapToEntity(dto, new CustomerAccount());
            var serviceResp = await _accountService.AddCustomerAccountAsync(mappedData).ConfigureAwait(false);
            return serviceResp;
        }

        public async Task<ServiceResult<CustomerAccount>> MapAndUpdateCustomerAsync(AccountDto dto)
        {
            var oldData = await _accountService.GetCustomerByIdAsync(dto.Id).ConfigureAwait(false);
            if (!oldData.Status)
                return oldData;

            var mappedData = MapToEntity(dto, oldData.Data);

            var serviceResp = await _accountService.UpdateCustomerAccountAsync(mappedData).ConfigureAwait(false);
            return serviceResp;
        }

        #endregion


        #region Mapping

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public ServiceResult<List<AccountDto>> MapAndGetCustomerAccount(List<CustomerAccount> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return new ServiceResult<List<AccountDto>>(true);
            }

            var results = new List<AccountDto>();

            foreach (var entity in entities)
            {
                var result = new AccountDto
                {
                    Id = entity.Id,
                    AccountNumber = entity.AccountNumber,
                    Name = entity.Name,
                    Email = entity.Email,
                    TotalAmount = entity.TotalAmount,
                    Status = entity.Status
                };

                results.Add(result);
            }

            return ServiceResult<List<AccountDto>>.Success(results);
        }



        private CustomerAccount MapToEntity(AccountDto dto, CustomerAccount entity)
        {
            entity.Id = dto.Id;
            entity.AccountNumber = string.IsNullOrWhiteSpace(dto.AccountNumber) ? GenerateAccountNumber() : dto.AccountNumber;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.TotalAmount = dto.TotalAmount;
            entity.Status = false;

            return entity;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            string accountNumber;
            do
            {
                accountNumber = "ACC" + random.Next(100000, 999999).ToString();
            } while (_accountRepository.Table.Any(x => x.AccountNumber == accountNumber));

            return accountNumber;
        }
        #endregion
    }
}
