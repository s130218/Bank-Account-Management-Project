using AccountManagement.Web.Models.DTO.Account;
using AccountManagement.Web.Models.DTO.Common;

namespace AccountManage.Web.Service
{
    public interface IAccountService
    {
        Task<ServiceResult<object>> GetAllAccountAsync();
        Task<ServiceResult> AddNewAccountAsync(AccountDto account);
        Task<ServiceResult> UpdateAccountStatus(int id);
    }
}
