using AccountManage.Web.Enum;
using AccountManage.Web.Service;
using AccountManagement.Web.Models.DTO.Common;
using AccountManagement.Web.Models.DTO.Statement;

namespace AccountManagement.Web.Service
{
    public class StatementService : IStatementService
    {
        private readonly IBaseService _baseService;
        public StatementService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ServiceResult<object>> GetAllStatementAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = "/v1/customer/statement"
            });
        }

        public async Task<ServiceResult> AddTranscationAsync(StatementDto account)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = "/v1/customer/statement",
                Data = account
            });
        }
    }
}
