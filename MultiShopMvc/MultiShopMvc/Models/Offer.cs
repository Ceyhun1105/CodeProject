using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShopMvc.Models
{
    public class Offer : BaseEntity
    {
        [StringLength(maximumLength: 100)]

        public string Title { get; set; }
        [StringLength(maximumLength: 1500)]

        public string Description { get; set; }
        [StringLength(maximumLength: 100)]

        public string RedirectUrl { get; set; }
        [StringLength(maximumLength: 100)]

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }


    }
}
