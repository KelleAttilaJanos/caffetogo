using System.Collections.Generic;

namespace caffetogo.Models
{
    public class AdminView
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Buy> buys { get; set; }
        public IEnumerable<UserView> users { get; set; }
    }
}
