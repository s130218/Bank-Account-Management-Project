using AccountManage.Web.Enum;
using AccountManage.Web.Service;
using AccountManagement.Web.Models.DTO.Auth;
using AccountManagement.Web.Models.DTO.Common;
using Newtonsoft.Json;

namespace AccountManagement.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IBaseService baseService, IHttpContextAccessor contextAccessor)
        {
            _baseService = baseService;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResult> RegisterUserAsync(RegistrationRequestDTO dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = "/v1/auth/register",
                Data = dto
            });
        }

        public async Task<ServiceResult> LoginAsync(LoginRequestDto dto)
        {
            var resp = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = "/v1/auth/login",
                Data = dto
            });

            if (resp.Status)
            {
                var res = JsonConvert.DeserializeObject<LoginResponseDto>(resp.Data.ToString() ?? "{ }");
                _contextAccessor.HttpContext?.Response.Cookies.Append("JwtToken", res.Token);
            }

            return resp;
        }

        public void Logout()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete("JwtToken");
        }

        public async Task<ServiceResult<object>> GetAllUsersAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = "/v1/auth/users"
            });
        }

        public async Task<ServiceResult> DeleteUserAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = $"/v1/auth/users/{userId}"
            });
        }

        public async Task<ServiceResult> UpdateUserRoleAsync(AssignNewRoleDTO dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Url = "/v1/auth/role/assign",
                Data = dto
            });
        }
    }
}
