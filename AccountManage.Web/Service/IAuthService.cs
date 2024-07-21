using AccountManagement.Web.Models.DTO.Auth;
using AccountManagement.Web.Models.DTO.Common;

namespace AccountManagement.Web.Service
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterUserAsync(RegistrationRequestDTO dto);
        Task<ServiceResult> LoginAsync(LoginRequestDto dto);
        void Logout();
        Task<ServiceResult<object>> GetAllUsersAsync();
    }
}
