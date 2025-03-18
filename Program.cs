using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<Users, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8; //i'm just following the video so we can discuss the
                                                     //password legnth eventually or scratch the idea or if we want them to use a pin style instead
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = true; ////so they have to confirm their email
                options.SignIn.RequireConfirmedAccount = true; //so they have to confirm their email
                                                               // options.Lockout.MaxFailedAccessAttempts = 5; lockout after 5 failed attempts interesting idea but we don't need to do that
                options.User.RequireUniqueEmail = true; //so once account = one email
                options.SignIn.RequireConfirmedPhoneNumber = false; //so they don't have to confirm their phone number
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //so they can use these characters in their username
                //IDk if the above code is required but it's there for now
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
