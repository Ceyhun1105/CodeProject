using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Products.AsQueryable();
            var products = PaginatedList<Product>.GetValues(query,5,page);
            return View(products);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            return RedirectToAction ("Index");
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}
