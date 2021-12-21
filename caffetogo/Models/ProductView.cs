using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;

namespace caffetogo.Models
{
    public class ProductView
    {
        [Key]
        public int Id { get; set; }
        public string item { get; set; }
        public int price { get; set; }
        public Image Pictures { get; set; }
    }
}