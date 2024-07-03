using System;
using System.Collections.Generic;

namespace ProjectPRN212.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }
}
