namespace caffetogo.Models
{
    /// <summary>
    /// A Bejelentkezés oldal viewmodelje
    /// </summary>
    public class Logins
    {
        /// <summary>
        /// A kosár tartalma (cart)
        /// </summary>
        public string bucket { get; set; }
        /// <summary>
        /// Az elmentett email cím
        /// </summary>
        public string email { get; set; }
    }
}