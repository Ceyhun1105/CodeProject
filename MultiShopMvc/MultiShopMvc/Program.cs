using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShopMvc.Areas.Admin.Services;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Models;
using MultiShopMvc.Services;

namespace MultiShopMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddScoped<AdminService>();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer("Server=.\\SQLEXPRESS;Database=MShop;Trusted_Connection=True");
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength= 8;
                opt.User.RequireUniqueEmail = false;


            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=AdminManager}/{action=Login}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}