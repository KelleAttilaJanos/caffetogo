using System.ComponentModel.DataAnnotations;

namespace caffetogo.Models
{
    public class Buy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Items { get; set; }
    }
}
