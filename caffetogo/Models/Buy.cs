using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    public class Buy
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Items { get; set; }
    }
}
