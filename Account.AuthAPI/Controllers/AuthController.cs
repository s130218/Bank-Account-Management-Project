using Account.AuthAPI.Dto.Auth;
using Account.AuthAPI.Dto.Common;
using Account.AuthAPI.Models.Auth;
using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.AuthAPI.Controllers
{
    [Route("v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authServcie)
        {
            _authService = authServcie;
        }


        #region Method

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDTO model)
        {
            var resp = await _authService.RegisterNewUserAsynx(model).ConfigureAwait(false);
            return Ok(resp);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resp = await _authService.LoginAsync(model).ConfigureAwait(false);
            return Ok(resp);
        }

        [Route("users")]
        [HttpGet]
        [Authorize(Policy = AuthorizePolicy.AdminRole)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync().ConfigureAwait(false);
            return Ok(users);
        }

        [Route("users/{userId}")]
        [HttpDelete]
        [Authorize(Policy = AuthorizePolicy.SuperAdminRole)]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var users = await _authService.DeleteUserAsync(userId).ConfigureAwait(false);
            return Ok(users);
        }


        [Route("role/assign")]
        [HttpPut]
        [Authorize(Policy = AuthorizePolicy.SuperAdminRole)]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] AssignNewRoleDTO model)
        {
            var resp = await _authService.AssignNewRoleAsync(model.UserId, model.Role.ToUpper());
            return Ok(resp);
        }
        #endregion
    }
}
