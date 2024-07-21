using Account.AuthAPI.Dto.Auth;
using Account.AuthAPI.Models.Auth;
using Account.AuthAPI.Service.Common;
using MediaBrowser.Model.Dto;
using Microsoft.AspNetCore.Components.Web;

namespace Account.AuthAPI.Service.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterNewUserAsynx(RegistrationRequestDTO registrationRequestDTO);
        Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginModel loginResponseDto);
        Task<ServiceResult<List<ApplicationUserDTO>>> GetAllUsersAsync();
        Task<ServiceResult> DeleteUserAsync(string userId);
        Task<ServiceResult> AssignNewRoleAsync(string email, string rolename);
    }
}
