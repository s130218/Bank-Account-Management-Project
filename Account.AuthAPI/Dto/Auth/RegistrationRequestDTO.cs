using Microsoft.Identity.Client;

namespace Account.AuthAPI.Dto.Auth
{
    public class RegistrationRequestDTO
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }

    public class AssignNewRoleDTO
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
