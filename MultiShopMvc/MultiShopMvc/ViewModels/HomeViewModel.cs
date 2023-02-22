using MultiShopMvc.Models;

namespace MultiShopMvc.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Offer> Offers { get; set; }
        public List<Service> Services { get; set; }
        public List<Category> Categories { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public List<Product> DiscountedProducts { get; set; }

    }
}
