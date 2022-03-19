using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    /// <summary>
    /// Adatbázis model Buy tábla
    /// </summary>
    public class Buy
    {
        /// <summary>
        /// A vásárlás azonosítója
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// A felhasználó azonosítója (vásárláshoz)
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// A vásárolni kívánt termékek azonosítói vesszővel elválasztva
        /// </summary>
        [Required]
        public string Items { get; set; }
    }
}