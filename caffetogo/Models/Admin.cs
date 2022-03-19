namespace caffetogo.Models
{
    /// <summary>
    /// Az Admin oldal beléptető modelje
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Az elfogadható Email
        /// </summary>
        public string AdminEmail = "Admin@admin.hu";
        /// <summary>
        /// Az elfogadható jelszó
        /// </summary>
        public string AdminPassword = "Admin";
        /// <summary>
        /// A megadott Email cím
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// A megadott jelszó
        /// </summary>
        public string Password { get; set; }
    }
}