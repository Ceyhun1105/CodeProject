using MultiShopMvc.Models.Base;
using MultiShopMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models
{

    public class Review : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public Rating Rating { get; set; }
        [StringLength(maximumLength: 1500)]
        public string Comment { get; set; }
        public Product? Product { get; set; }
        public AppUser? AppUser { get; set; }

    }

    
}

