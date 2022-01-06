using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
namespace caffetogo.Models
{
    public class Check
    {
        public int Id { get; set; }
        public string Cart { get; set; }
        public List<Product> Products { get; set; }
        public string Email { get; set; }
    }
}
