﻿using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Web.Models.DTO.Auth
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class AssignNewRoleDTO
    {
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
