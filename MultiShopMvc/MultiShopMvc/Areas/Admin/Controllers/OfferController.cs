using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public OfferController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Offers.AsQueryable();
            var offer = PaginatedList<Offer>.GetValues(query, 2, page);
            return View(offer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Offer offer)
        {
            if (!ModelState.IsValid) return View(offer);

            if (offer.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Required");
                return View(offer);
            }
            if (!offer.ImageFile.CheckFileLength(3 * 1024576))
            {
                ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                return View(offer);
            }
            if (!offer.ImageFile.CheckFileType())
            {
                ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                return View(offer);
            }

            offer.ImageUrl = offer.ImageFile.SaveFile(_env.WebRootPath, "uploads/offer");
            offer.CreatedTime = DateTime.UtcNow;
            _context.Offers.Add(offer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Offer existoffer = _context.Offers.FirstOrDefault(x => x.Id == id);
            if (existoffer is null) return NotFound();
            ViewBag.Image = existoffer.ImageUrl;
            return View(existoffer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Offer offer)
        {
            Offer existoffer = _context.Offers.FirstOrDefault(x => x.Id == offer.Id);

            ViewBag.Image = existoffer.ImageUrl;
            if (!ModelState.IsValid) return View(offer);

            if (offer.ImageFile != null)
            {
                if (!offer.ImageFile.CheckFileLength(3 * 1024576))
                {
                    ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                    return View(offer);
                }
                if (!offer.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                    return View(offer);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/offer", existoffer.ImageUrl);
                if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                existoffer.ImageUrl = offer.ImageFile.SaveFile(_env.WebRootPath, "uploads/offer");
            }

            existoffer.Description = offer.Description;
            existoffer.Title = offer.Title;
            existoffer.RedirectUrl = offer.RedirectUrl;
            existoffer.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Offer existoffer = _context.Offers.FirstOrDefault(c => c.Id == id);
            if (existoffer is null) return NotFound();
            existoffer.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id)
        {
            Offer existoffer = _context.Offers.FirstOrDefault(c => c.Id == id);
            if (existoffer is null) return NotFound();
            existoffer.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Offer existoffer = _context.Offers.FirstOrDefault(c => c.Id == id);
            if (existoffer is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "uploads/offer", existoffer.ImageUrl);
            if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            _context.Remove(existoffer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
