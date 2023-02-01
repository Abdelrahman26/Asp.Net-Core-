using System.ComponentModel.DataAnnotations;

namespace WebSecurityLec4_ITI.DTOs
{
    public class RegisterCredentials
    {
        // there is no need to validation here, user manager(identity) handle it internally.
        // you can override options of identity in program.cs to customize it.
        // validators here is another option.

       // [Required]
        public string Username { get; set; } = "";
       // [Required]
        public string Password { get; set; } = "";
       // [Required]
       // [EmailAddress]
        public string Email { get; set; } = "";
        public string Department { get; set; } = "";
    }
}
