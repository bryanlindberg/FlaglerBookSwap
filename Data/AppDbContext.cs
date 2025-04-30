using FlaglerBookSwap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FlaglerBookSwap.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
        //I believe this is the thing that actually connects the class "Textbooks" to the table "Textbooks"
        public DbSet<Textbooks> Textbooks { get; set; }
        public DbSet<Courses_Textbooks> Courses_Textbooks { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Listings> Listings { get; set; }
        public DbSet<ContactForms> ContactForms { get; set; }
    }

}