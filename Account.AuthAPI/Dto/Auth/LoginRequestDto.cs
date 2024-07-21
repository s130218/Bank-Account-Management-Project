using System.Runtime.CompilerServices;

namespace Account.AuthAPI.Dto.Auth
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
