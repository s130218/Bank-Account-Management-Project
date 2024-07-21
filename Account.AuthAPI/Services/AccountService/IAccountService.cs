using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Service.Common;
using BankAccountAPI.Model;

namespace BankAccountAPI.Services.Service
{
    public interface IAccountService
    {
        Task<ServiceResult<List<CustomerAccount>>> GetAllAsync();

        Task<ServiceResult<CustomerAccount>> GetCustomerByIdAsync(int id);

        Task<ServiceResult<CustomerAccount>> AddCustomerAccountAsync(CustomerAccount entity);

        Task<ServiceResult<CustomerAccount>> UpdateCustomerAccountAsync(CustomerAccount entity);

        Task<ServiceResult<CustomerAccount>> UpdateCustomerStatusAsync(int id);

        
    }
}
