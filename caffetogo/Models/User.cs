using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;

namespace caffetogo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public DateTime Activity { get; set; }        
    }
}