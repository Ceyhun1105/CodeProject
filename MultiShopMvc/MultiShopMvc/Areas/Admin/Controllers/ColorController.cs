using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ColorController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ColorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Colors.AsQueryable();
            var color = PaginatedList<Color>.GetValues(query, 2, page);
            return View(color);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid) return View(color);
            color.CreatedTime = DateTime.UtcNow;
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Color existcolor = _context.Colors.FirstOrDefault(x => x.Id == id);
            if (existcolor is null) return NotFound();
            return View(existcolor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Color color)
        {
            Color existcolor = _context.Colors.FirstOrDefault(x => x.Id == color.Id);
            if (!ModelState.IsValid) return View(color);
            existcolor.Name = color.Name;
            existcolor.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Color existcolor = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (existcolor is null) return NotFound();
            existcolor.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Color existcolor = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (existcolor is null) return NotFound();
            existcolor.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Color existcolor = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (existcolor is null) return NotFound();
            _context.Remove(existcolor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
