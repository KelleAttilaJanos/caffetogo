using System;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// A felhasználó Viewmodelle
    /// </summary>
    public class UserView
    {
        /// <summary>
        /// A felhasználó azonosítója
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// A felhasználó Email címe
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// A felhasználó Jelszava
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// A felhasználó regisztrációjának időpontja (utólag módosítható)
        /// </summary>
        public DateTime Activity { get; set; }
        /// <summary>
        /// A kosár tartalma
        /// </summary>
        public string cart { get; set; }
    }
}