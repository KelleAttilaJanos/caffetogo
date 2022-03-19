using System.Collections.Generic;

namespace caffetogo.Models
{
    /// <summary>
    /// Az Admin oldal Viewmodelje
    /// </summary>
    public class AdminView
    {
        /// <summary>
        /// A termékek listája
        /// </summary>
        public IEnumerable<Product> products { get; set; }
        /// <summary>
        /// A vásárlások listája
        /// </summary>
        public IEnumerable<Buy> buys { get; set; }
        /// <summary>
        /// A felhasználók listája
        /// </summary>
        public IEnumerable<UserView> users { get; set; }
    }
}