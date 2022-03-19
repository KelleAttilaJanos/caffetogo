using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// A termékek Viewmodelje
    /// </summary>
    public class ProductView
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
        [Range(1, int.MaxValue, ErrorMessage = "Nagyobbnak kell lennie mint 0")]
        public int price { get; set; }
        /// <summary>
        /// A letöltött kép
        /// </summary>
        public IFormFile Pictures { get; set; }
    }
}