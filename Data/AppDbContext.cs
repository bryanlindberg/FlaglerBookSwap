using FlaglerBookSwap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


public class AppDbContext : IdentityDbContext<Users>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    { }
    //I believe this is the thing that actually connects the class "Textbooks" to the table "Textbooks"
    public DbSet<Textbooks> Textbooks { get; set; }
    public DbSet<Courses> Courses { get; set; }

    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Rename Identity tables to match your schema
        builder.Entity<Users>(entity => {
            entity.ToTable("Users");  // Instead of AspNetUsers
        });

        // You also need to do this for all other Identity tables
        builder.Entity<IdentityRole>(entity => {
            entity.ToTable("Roles");  // Instead of AspNetRoles
        });

        builder.Entity<IdentityUserRole<string>>(entity => {
            entity.ToTable("UserRoles");  // Instead of AspNetUserRoles
        });

        builder.Entity<IdentityUserClaim<string>>(entity => {
            entity.ToTable("UserClaims");  // Instead of AspNetUserClaims
        });

        builder.Entity<IdentityUserLogin<string>>(entity => {
            entity.ToTable("UserLogins");  // Instead of AspNetUserLogins
        });

        builder.Entity<IdentityRoleClaim<string>>(entity => {
            entity.ToTable("RoleClaims");  // Instead of AspNetRoleClaims
        });

        builder.Entity<IdentityUserToken<string>>(entity => {
            entity.ToTable("UserTokens");  // Instead of AspNetUserTokens
        });

        // Your other entity configurations
        // ...
    }

}
