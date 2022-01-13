using System.Collections.Generic;
namespace caffetogo.Models
{
    public class Check
    {
        public int Id { get; set; }
        public string Cart { get; set; }
        public List<Product> Products { get; set; }
        public string Email { get; set; }
        public string message { get; set; }
    }
}
