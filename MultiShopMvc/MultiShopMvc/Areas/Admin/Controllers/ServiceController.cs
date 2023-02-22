using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Services.AsQueryable();
            var service = PaginatedList<Service>.GetValues(query, 2, page);
            return View(service);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid) return View(service);
            service.CreatedTime = DateTime.UtcNow;
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Service existservice = _context.Services.FirstOrDefault(x => x.Id == id);
            if (existservice is null) return NotFound();
            return View(existservice);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Service service)
        {
            Service existservice = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (!ModelState.IsValid) return View(service);
            existservice.Name = service.Name;
            existservice.Icon = service.Icon;
            existservice.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Service existservice = _context.Services.FirstOrDefault(c => c.Id == id);
            if (existservice is null) return NotFound();
            existservice.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Service existservice = _context.Services.FirstOrDefault(c => c.Id == id);
            if (existservice is null) return NotFound();
            existservice.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Service existservice = _context.Services.FirstOrDefault(c => c.Id == id);
            if (existservice is null) return NotFound();
            _context.Remove(existservice);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
