using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShopMvc.Models
{
    public class ProductImages : BaseEntity
    {
        public int ProductId { get; set; }
        [StringLength(maximumLength: 100)]

        public string? ImageUrl { get; set; }
        public bool IsPoster { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public Product? Product { get; set; }
    }
}
