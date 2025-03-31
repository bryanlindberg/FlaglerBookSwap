using FlaglerBookSwap.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    { }
    //I believe this is the thing that actually connects the class "Textbooks" to the table "Textbooks"
    public DbSet<Textbooks> Textbooks { get; set; }
    public DbSet<Courses> Courses { get; set; }
}
