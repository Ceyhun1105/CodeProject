using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShopMvc.Models;

namespace MultiShopMvc.DbContextFiles;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options)
    {
            
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }

}
