using Account.AuthAPI.Dto.Auth;
using Account.AuthAPI.Models.Auth;
using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Service.Common;
using Microsoft.AspNetCore.Identity;

namespace Account.AuthAPI.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ServiceResult> RegisterNewUserAsynx(RegistrationRequestDTO registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (!result.Succeeded)
            {
                List<string> errorList = [];
                foreach (var error in result.Errors)
                    errorList.Add(error.Description.ToString());

                return ServiceResult.Fail(errorList);
            }

            await _userManager.AddToRoleAsync(user, FixedRoles.Customer);

            return ServiceResult.Success("User registered successfully");
        }

        public async Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (!result.Succeeded)
                return ServiceResult<LoginResponseDto>.Fail("Invalid login attempt");

            var user = await _userManager.FindByNameAsync(model.Username);
            var (tokenString, expiryMinute) = await _jwtTokenGenerator.GenerateToken(user);

            return new ServiceResult<LoginResponseDto>(true)
            {
                Message = ["Login successful"],
                Data = new LoginResponseDto
                {
                    Token = tokenString,
                    ExpiryMinutes = expiryMinute
                }
            };
        }

        public async Task<ServiceResult<List<ApplicationUserDTO>>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<ApplicationUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                userDtos.Add(new ApplicationUserDTO
                {
                    UserId = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles
                });
            }

            return ServiceResult<List<ApplicationUserDTO>>.Success(userDtos);
        }

        public async Task<ServiceResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResult.Fail("User not found.");

            // Delete the user
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return ServiceResult.Fail(result.Errors.First().Description);
            }

            return ServiceResult.Success("User has been deleted successfully.");
        }

        public async Task<ServiceResult> AssignNewRoleAsync(string userId, string newRoleName)
        {
            var validation = ValidateRole(newRoleName);
            if (!validation.Status)
                return validation;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResult.Fail("User not found.");

            var existingRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);

            if (!removeResult.Succeeded)
                return ServiceResult.Fail("Failed to remove existing roles.");

            var addResult = await _userManager.AddToRoleAsync(user, newRoleName);
            if (!addResult.Succeeded)
                return ServiceResult.Fail("Failed to add new role.");

            return ServiceResult.Success("User role has been updated successfully.");
        }

        public ServiceResult ValidateRole(string role)
        {
            List<string> allowedRoles = FixedRoles.GetAllRoles();

            var isExist = allowedRoles.Any(x => x.ToUpper() == role.ToUpper());
            if (!isExist)
                return ServiceResult.Fail("Assigned role in not valid.");

            return new ServiceResult(true);
        }
    }
}
