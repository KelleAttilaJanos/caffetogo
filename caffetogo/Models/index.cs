using caffetogo.Data;
using caffetogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
namespace caffetogo.Models

{
    public class index
    {
        public string Email { get; set; }
        public int id { get; set; }
        public bool loggedin { get; set; }
        public List<Product> product { get; set; }

    }
}
