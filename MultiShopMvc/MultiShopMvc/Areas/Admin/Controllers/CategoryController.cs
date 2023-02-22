using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Categories.AsQueryable();
            var category = PaginatedList<Category>.GetValues(query, 2, page);
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid) return View(category);

            Category category1 = _context.Categories.FirstOrDefault(x => x.Name == category.Name);
            if(category1 is not null)
            {
                ModelState.AddModelError("Name", "Already exist");
                return View(category);
            }

            if (category.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile","Required");
                return View(category);
            }
            if (!category.ImageFile.CheckFileLength(3 * 1024576))
            {
                ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                return View(category);
            }
            if (!category.ImageFile.CheckFileType())
            {
                ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                return View(category);
            }

            category.ImageUrl = category.ImageFile.SaveFile(_env.WebRootPath, "uploads/category");
            category.CreatedTime= DateTime.UtcNow;
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

        public IActionResult Update(int id)
        {
            Category existcategory = _context.Categories.FirstOrDefault(x=> x.Id == id);
            if(existcategory is null) return NotFound();
            ViewBag.Image = existcategory.ImageUrl;
            return View(existcategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(Category category)
        {
            Category existcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            Category category1 = _context.Categories.FirstOrDefault(x => x.Name== category.Name);
            ViewBag.Image = existcategory.ImageUrl;
            if (!ModelState.IsValid) return View(category);

            if(category1 is not null && category1.Name != category.Name)
            {
                ModelState.AddModelError("Name", "Already exist");
                return View(category1);
            }

            if (category.ImageFile!= null)
            {
                if (!category.ImageFile.CheckFileLength(3 * 1024576))
                {
                    ModelState.AddModelError("ImageFile", "Please,upload less than 3 Mb file.");
                    return View(category);
                }
                if (!category.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please,upload only jpg/jpeg/png file.");
                    return View(category);
                }
                string path = Path.Combine(_env.WebRootPath,"uploads/category",existcategory.ImageUrl);
                if(System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                existcategory.ImageUrl = category.ImageFile.SaveFile(_env.WebRootPath, "uploads/category");
            }

            existcategory.Name= category.Name;
            existcategory.ModifiedTime = DateTime.UtcNow;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult SoftDelete(int id)
        {
            Category existcategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(existcategory is null) return NotFound();
            existcategory.IsDeleted= true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult GetBack(int id) 
        {
            Category existcategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existcategory is null) return NotFound();
            existcategory.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category existcategory = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existcategory is null) return NotFound();

            List<Product> products = _context.Products.Where(x=>x.CategoryId== id).ToList();
            foreach(Product product in products)
            {
                if(product.Images is not null)
                {
                    foreach (var item in product.Images)
                    {
                        string path1 = Path.Combine(_env.WebRootPath, "uploads/product", item.ImageUrl);
                        if (System.IO.File.Exists(path1)){ System.IO.File.Delete(path1); }
                    }
                }
                
            }


            string path = Path.Combine(_env.WebRootPath, "uploads/category", existcategory.ImageUrl);
            if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
            _context.Remove(existcategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
