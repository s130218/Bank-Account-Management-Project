using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Web.Models.DTO.Auth
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
