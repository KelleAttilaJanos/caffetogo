using caffetogo.Models;
using Microsoft.EntityFrameworkCore;

namespace caffetogo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Buy> Buy { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
    }
}