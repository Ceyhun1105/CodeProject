using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Models;
using MultiShopMvc.ViewModels;

namespace MultiShopMvc.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserManagerController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userVm)
        {
            if (!ModelState.IsValid) return View(userVm);

            AppUser member = await _userManager.FindByNameAsync(userVm.UserName);
            if (member is null)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View(userVm);
            }
            var result = await _signInManager.PasswordSignInAsync(member, userVm.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View(userVm);
            }
            return RedirectToAction("index", "home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser exist = await _userManager.FindByNameAsync(registerVM.UserName);
            if (exist is not null)
            {
                ModelState.AddModelError("UserName", "Alredy Exist!");
                return View(registerVM);
            }
            exist = _context.Users.FirstOrDefault(x => x.NormalizedEmail == registerVM.Email.ToUpper());
            if (exist != null)
            {
                ModelState.AddModelError("Email", "Already Exist!");
                return View(registerVM);
            }

            AppUser user = new AppUser()
            {
                UserName= registerVM.UserName,
                Email= registerVM.Email,
                FullName= registerVM.FullName,
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View(registerVM);
                }
            }

            result = await _userManager.AddToRoleAsync(user, "Member");
            if(!result.Succeeded) 
            {
                return View(registerVM);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
           
            return RedirectToAction("Login", "UserManager");
        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "home");
        }
    }
}
