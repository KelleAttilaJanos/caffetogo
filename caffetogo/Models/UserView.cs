using System;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    public class UserView
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime Activity { get; set; }
        public string cart { get; set; }
    }
}