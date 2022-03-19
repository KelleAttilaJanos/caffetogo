using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// Adatbázis model Product tábla
    /// </summary>
    public class Product
    {
        /// <summary>
        /// A termékek azonosítója
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// A termékek neve
        /// </summary>
        [Required]
        public string item { get; set; }
        /// <summary>
        /// A termékek ára
        /// </summary>
        [Required]
        [Display(Name = "Price")]
        [Range(1, int.MaxValue, ErrorMessage = "Nagyobbnak kell lennie mint 0")]
        public int price { get; set; }
    }
}