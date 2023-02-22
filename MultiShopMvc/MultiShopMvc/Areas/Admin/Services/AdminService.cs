using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Models;

namespace MultiShopMvc.Areas.Admin.Services
{
    public class AdminService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminService(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AppUser> GetActiveUser(string Username)
        {
            AppUser activeuser = await _userManager.FindByNameAsync(Username);
            return activeuser;
        }

    }
}
