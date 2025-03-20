using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

    }
}
