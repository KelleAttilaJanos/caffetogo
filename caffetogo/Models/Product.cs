using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;

namespace caffetogo.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string item { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(1, int.MaxValue,ErrorMessage = "Nagyobbnak kell lennie mint 0")]
        public int price { get; set; }
    }
}
