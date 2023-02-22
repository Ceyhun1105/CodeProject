using MultiShopMvc.DbContextFiles;
using MultiShopMvc.Models;

namespace MultiShopMvc.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public List<Setting> GetSettings()
        {
            return _context.Settings.ToList();
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.Where(x=>x.IsDeleted==false).ToList();
        }

        public AppUser GetUser(string username)
        {
            AppUser user = _context.Users.FirstOrDefault(x => x.UserName == username);
            return user;
        }

    }
}
