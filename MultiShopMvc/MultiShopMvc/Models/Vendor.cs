using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShopMvc.Models
{
    public class Vendor : BaseEntity
    {

        [StringLength(maximumLength: 100)]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
