using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShopMvc.Models
{
    public class Category : BaseEntity
    {

        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(maximumLength: 150)]
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public List<Product>? Products { get; set; }
    }
}
