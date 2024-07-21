namespace AccountManagement.Web.Models.DTO.Auth
{
    public class LoginResponseDto
    {
        // public UserDto User { get; set; }

        public string Token { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
