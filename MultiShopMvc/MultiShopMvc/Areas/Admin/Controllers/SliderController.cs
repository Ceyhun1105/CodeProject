using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Sliders.AsQueryable();
            var slider = PaginatedList<Slider>.GetValues(query, 2, page);
            return View(slider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);

            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Required");
                return View(slider);
            }
            if (!slider.ImageFile.CheckFileLength(3 * 1024576))
            {
                ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                return View(slider);
            }
            if (!slider.ImageFile.CheckFileType())
            {
                ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                return View(slider);
            }

            slider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/slider");
            slider.CreatedTime = DateTime.UtcNow;
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (existslider is null) return NotFound();
            ViewBag.Image = existslider.ImageUrl;
            return View(existslider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Slider slider)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            ViewBag.Image = existslider.ImageUrl;
            if (!ModelState.IsValid) return View(slider);

            if (slider.ImageFile != null)
            {
                if (!slider.ImageFile.CheckFileLength(3 * 1024576))
                {
                    ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                    return View(slider);
                }
                if (!slider.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                    return View(slider);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/slider", existslider.ImageUrl);
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                existslider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/slider");
            }

            existslider.Description = slider.Description;
            existslider.Title = slider.Title;
            existslider.RedirectUrl= slider.RedirectUrl;
            existslider.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(c => c.Id == id);
            if (existslider is null) return NotFound();
            existslider.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(c => c.Id == id);
            if (existslider is null) return NotFound();
            existslider.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(c => c.Id == id);
            if (existslider is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "uploads/slider", existslider.ImageUrl);
            if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            _context.Remove(existslider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
