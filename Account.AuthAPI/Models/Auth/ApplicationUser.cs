using Microsoft.AspNetCore.Identity;

namespace Account.AuthAPI.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
