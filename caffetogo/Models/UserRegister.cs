using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, PasswordPropertyText]
        public string Password { get; set; }
        [Required, PasswordPropertyText]
        public string confirmpassword { get; set; }
        public string cart { get; set; }
    }
}
