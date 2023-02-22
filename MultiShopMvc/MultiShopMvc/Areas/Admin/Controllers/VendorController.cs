using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public VendorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Vendors.AsQueryable();
            var vendor = PaginatedList<Vendor>.GetValues(query, 2, page);
            return View(vendor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor vendor)
        {
            if (!ModelState.IsValid) return View(vendor);

            if (vendor.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Required");
                return View(vendor);
            }
            if (!vendor.ImageFile.CheckFileLength(3 * 1024576))
            {
                ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                return View(vendor);
            }
            if (!vendor.ImageFile.CheckFileType())
            {
                ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                return View(vendor);
            }

            vendor.ImageUrl = vendor.ImageFile.SaveFile(_env.WebRootPath, "uploads/vendor");
            vendor.CreatedTime = DateTime.UtcNow;
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Vendor existvendor = _context.Vendors.FirstOrDefault(x => x.Id == id);
            if (existvendor is null) return NotFound();
            ViewBag.Image = existvendor.ImageUrl;
            return View(existvendor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Vendor vendor)
        {
            Vendor existvendor = _context.Vendors.FirstOrDefault(x => x.Id == vendor.Id);

            ViewBag.Image = existvendor.ImageUrl;
            if (!ModelState.IsValid) return View(vendor);

            if (vendor.ImageFile != null)
            {
                if (!vendor.ImageFile.CheckFileLength(3 * 1024576))
                {
                    ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                    return View(vendor);
                }
                if (!vendor.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                    return View(vendor);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/vendor", existvendor.ImageUrl);
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                existvendor.ImageUrl = vendor.ImageFile.SaveFile(_env.WebRootPath, "uploads/vendor");
            }
            existvendor.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Vendor existvendor = _context.Vendors.FirstOrDefault(c => c.Id == id);
            if (existvendor is null) return NotFound();
            existvendor.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Vendor existvendor = _context.Vendors.FirstOrDefault(c => c.Id == id);
            if (existvendor is null) return NotFound();
            existvendor.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Vendor existvendor = _context.Vendors.FirstOrDefault(c => c.Id == id);
            if (existvendor is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "uploads/vendor", existvendor.ImageUrl);
            if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            _context.Remove(existvendor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
