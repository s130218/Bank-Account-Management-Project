using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Service.Common;
using BankAccountAPI.DTO;

namespace BankAccountAPI.Factory
{
    public interface IAccountFactory
    {
        ServiceResult<List<AccountDto>> MapAndGetCustomerAccount(List<CustomerAccount> entities);

        Task<ServiceResult<CustomerAccount>> MapAndAddCustomerAccount(AccountDto dto);

        Task<ServiceResult<CustomerAccount>> MapAndUpdateCustomerAsync(AccountDto dto);
    }
}
