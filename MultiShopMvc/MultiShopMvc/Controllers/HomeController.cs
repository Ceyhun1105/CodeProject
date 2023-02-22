using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.ViewModels;

namespace MultiShopMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Categories = _context.Categories.Where(x => x.IsDeleted == false).ToList(),
                Services = _context.Services.Where(x => x.IsDeleted == false).ToList(),
                Sliders = _context.Sliders.Where(x => x.IsDeleted == false).ToList(),
                Offers = _context.Offers.Where(x => x.IsDeleted == false).ToList(),
                Vendors = _context.Vendors.Where(x=>x.IsDeleted==false).ToList(),
                FeaturedProducts = _context.Products.Where(x => x.IsFeatured == true && x.IsDeleted == false).Include(x=>x.Reviews).Include(x=>x.Images).ToList(),
                DiscountedProducts = _context.Products.Where(x=>x.DiscountPercent>0 && x.IsDeleted==false).Include(x => x.Reviews).Include(x => x.Images).ToList(),
            };
            return View(homeViewModel);
        }
    }
}