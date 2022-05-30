using Microsoft.EntityFrameworkCore;
using BooksWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BooksWeb.Data

{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
