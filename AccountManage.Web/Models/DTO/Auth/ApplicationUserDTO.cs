﻿namespace AccountManagement.Web.Models.DTO.Auth;

public class ApplicationUserDTO
{
    public string UserId { get; set; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public IList<string> Roles { get; set; }
}
