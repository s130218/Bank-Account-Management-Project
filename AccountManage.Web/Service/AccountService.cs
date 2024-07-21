using AccountManage.Web.Enum;
using AccountManagement.Web.Models.DTO.Account;
using AccountManagement.Web.Models.DTO.Common;

namespace AccountManage.Web.Service
{
    public class AccountService : IAccountService
    {
        private readonly IBaseService _baseService;
        public AccountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ServiceResult<object>> GetAllAccountAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = "/v1/customer/account"
            });
        }

        public async Task<ServiceResult> AddNewAccountAsync(AccountDto account)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = "/v1/customer/account",
                Data = account
            });
        }

        public async Task<ServiceResult> UpdateAccountStatus(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Url = $"/v1/customer/account/{id}"
            });
        }
    }
}
