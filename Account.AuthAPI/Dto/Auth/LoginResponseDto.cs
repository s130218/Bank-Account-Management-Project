using MediaBrowser.Model.Dto;

namespace Account.AuthAPI.Dto.Auth
{
    public class LoginResponseDto
    {
        // public UserDto User { get; set; }

        public string Token { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
