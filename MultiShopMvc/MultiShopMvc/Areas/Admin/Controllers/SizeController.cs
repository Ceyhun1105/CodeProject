using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SizeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Sizes.AsQueryable();
            var size = PaginatedList<Size>.GetValues(query, 2, page);
            return View(size);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid) return View(size);
            size.CreatedTime = DateTime.UtcNow;
            _context.Sizes.Add(size);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Size existsize = _context.Sizes.FirstOrDefault(x => x.Id == id);
            if (existsize is null) return NotFound();
            return View(existsize);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Size size)
        {
            Size existsize = _context.Sizes.FirstOrDefault(x => x.Id == size.Id);
            if (!ModelState.IsValid) return View(size);
            existsize.Name = size.Name;
            existsize.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Size existsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (existsize is null) return NotFound();
            existsize.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Size existsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (existsize is null) return NotFound();
            existsize.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Size existsize = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (existsize is null) return NotFound();
            _context.Remove(existsize);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
