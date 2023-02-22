using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.Areas.Admin.ViewModels;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Helpers;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public AdminManagerController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginViewModel adminVM)
        {
            if (!ModelState.IsValid) return View(adminVM);

            AppUser admin = await _userManager.FindByNameAsync(adminVM.UserName);
            if (admin is null)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View(adminVM);
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View(adminVM);
            }

            return RedirectToAction("index", "dashboard");

        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "home", new { area = "null" });
        }


        public IActionResult Settings()
        {
            AppUser activeuser = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (activeuser == null)
            {
                return RedirectToAction("index","home", new { area = "null" });
            }
            ViewBag.Image = activeuser.ImageUrl;
            return View(activeuser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(AppUser appUser)
        {
            if (!ModelState.IsValid) return View(appUser);

            AppUser existuser = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if(existuser == null)
            {
                return RedirectToAction("index", "home", new { area = "null" });
            }

            if (appUser.ImageFile != null)
            {
                if (!appUser.ImageFile.CheckFileLength(1024576 * 5))
                {
                    ModelState.AddModelError("ImageFile", "Please upload less than 5 Mb file");
                    return View(appUser);
                }
                if (!appUser.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please upload image file");
                    return View(appUser);
                }
                if (existuser?.ImageUrl != null)
                {
                    string path = Path.Combine(_env.WebRootPath, "uploads/user", existuser.ImageUrl);
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                }


                existuser.ImageUrl = appUser.ImageFile.SaveFile(_env.WebRootPath, "uploads/user");
            }
            if (appUser.Email is not null)
            {
                existuser.Email = appUser.Email;
            }
            if (appUser.PhoneNumber is not null)
            {
                existuser.PhoneNumber = appUser.PhoneNumber;
            }
            existuser.FullName = appUser.FullName;
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");

        }

      /*  public async Task<IActionResult> CreateRole()
        {
           
            IdentityRole role2 = new IdentityRole("Member");
            var result = await _roleManager.CreateAsync(role2);

            return Ok("Created");
        }*/




        /* public async Task<IActionResult> CreateAdmin()
         {
             AppUser admin = new AppUser
             {
                 FullName = "Ceyhun Farzullayev",
                 UserName = "Ceyhun1105",
             };
             var result = await _userManager.CreateAsync(admin, "Ceyhun1105");

             return Ok(result);
         }
         public async Task<IActionResult> CreateRole()
         {
             IdentityRole role1 = new IdentityRole("Admin");
             IdentityRole role2 = new IdentityRole("Member");

             _roleManager.CreateAsync(role1);
             _roleManager.CreateAsync(role2);

             return Ok("Created");
         }
        
         public async Task<IActionResult> SetRole()
         {
             AppUser admin = await _userManager.FindByNameAsync("Ceyhun1105");
             var result = await _userManager.AddToRoleAsync(admin, "Admin");

             return Ok(result);

         }*/
    }
}
