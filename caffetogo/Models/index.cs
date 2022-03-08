using System.Collections.Generic;
namespace caffetogo.Models
{
    public class index
    {
        public string Email { get; set; }
        public string id { get; set; }
        public bool loggedin { get; set; }
        public List<Product> product { get; set; }
        public string message { get; set; }
        public string cart { get; set; }
    }
}
