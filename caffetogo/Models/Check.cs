using System.Collections.Generic;
namespace caffetogo.Models
{
    /// <summary>
    /// A Check oldal Viewmodelje
    /// </summary>
    public class Check
    {
        /// <summary>
        /// A felhasználó azonosítója
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A kosár tartalma
        /// </summary>
        public string Cart { get; set; }
        /// <summary>
        /// A termékek listája
        /// </summary>
        public List<Product> Products { get; set; }
        /// <summary>
        /// A felhasználó Email címe
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// A hibaüzenet amit az oldalra belépéskor megjelenít a modalba
        /// </summary>
        public string message { get; set; }
    }
}