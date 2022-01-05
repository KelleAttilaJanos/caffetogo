using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;

namespace caffetogo.Models
{
    public class UserRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string confirmpassword {get;set;}



    }
}
