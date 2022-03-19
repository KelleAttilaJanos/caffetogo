using System.Collections.Generic;
namespace caffetogo.Models
{
    /// <summary>
    /// A Shop oldal Viewmodelje
    /// </summary>
    public class index
    {
        /// <summary>
        /// A Shop oldalon lévő gomb felirata (Email-cím)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// A felhasználó azonosítója
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// Annak a jelzése hogy az adott felhasználó be van e jelentkezve
        /// </summary>
        public bool loggedin { get; set; }
        /// <summary>
        /// A termékek listája
        /// </summary>
        public List<Product> product { get; set; }
        /// <summary>
        /// A hibaüzenet amit az oldalra belépéskor megjelenít a modalba
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// A kosár tartalma
        /// </summary>
        public string cart { get; set; }
    }
}