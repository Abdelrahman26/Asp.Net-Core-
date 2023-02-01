using Microsoft.AspNetCore.Identity;

namespace WebSecurityLec4_ITI.Data.Models;

public class Employee : IdentityUser
{
    public string Department { get; set; } = "";
}

