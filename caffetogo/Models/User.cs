using System;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// Adatbázis model User tábla
    /// </summary>
    public class User
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
        /// A felhasználó Jelszava (titkosított)
        /// </summary>
        [Required]
        public byte[] Password { get; set; }
        /// <summary>
        /// A felhasználó regisztrációjának időpontja
        /// </summary>
        public DateTime Activity { get; set; }
    }
}