using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;
using System.Data;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
            private readonly AppDbContext _context;

            public SettingController(AppDbContext context)
            {
                _context = context;
            }
            public IActionResult Index(int page = 1)
            {
                var query = _context.Settings.AsQueryable();
                List<Setting> settings = PaginatedList<Setting>.GetValues(query,5,page);
                return View(settings);
            }
            public IActionResult Update(int id)
            {
                Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
                if (setting == null) return NotFound();
                return View(setting);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Update(Setting setting)
            {
                Setting existsetting = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);
                if (!ModelState.IsValid) return View(setting);
                existsetting.Value = setting.Value;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }
}
