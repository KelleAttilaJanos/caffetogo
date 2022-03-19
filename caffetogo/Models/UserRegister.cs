using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// Regisztrációs Viewmodel
    /// </summary>
    public class UserRegister
    {
        /// <summary>
        /// Regisztrálásra használt Email cím
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// Regisztrálásra használt jelszó
        /// </summary>
        [Required, PasswordPropertyText]
        public string Password { get; set; }
        /// <summary>
        /// Regisztrálásra használt jelszó megerősítése 
        /// </summary>
        [Required, PasswordPropertyText]
        public string confirmpassword { get; set; }
        /// <summary>
        /// A kosár tartalma
        /// </summary>
        public string cart { get; set; }
    }
}