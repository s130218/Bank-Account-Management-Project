using AccountManagement.Web.Models.DTO.Common;

namespace AccountManage.Web.Service
{
    public interface IBaseService
    {
        Task<ServiceResult<object>> SendAsync(RequestDto requestDto);
    }
}
