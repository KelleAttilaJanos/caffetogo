namespace caffetogo.Models
{
    /// <summary>
    /// Az elfelejtett jelszó oldalhoz tartozó Viewmodel
    /// </summary>
    public class Fpw
    {
        /// <summary>
        /// A felhasználó azonosítója
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// A felhasználó módosított jelszava
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// A felhasználó módosított jelszó megerősítője
        /// </summary>
        public string confirmpassword { get; set; }
    }
}