using System;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public DateTime Activity { get; set; }
    }
}