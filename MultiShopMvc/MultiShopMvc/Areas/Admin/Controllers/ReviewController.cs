using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
