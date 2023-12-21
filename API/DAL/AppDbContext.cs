using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Book> Books {  get; set; }
        public DbSet<Category> Catagories{ get; set; }

    }
}
