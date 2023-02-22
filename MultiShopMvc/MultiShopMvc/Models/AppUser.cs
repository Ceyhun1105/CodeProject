using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiShopMvc.Models
{
    public class AppUser : IdentityUser
    {

        [StringLength(maximumLength:50)]
        public string FullName { get; set; }

        [StringLength(maximumLength: 150)]

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
