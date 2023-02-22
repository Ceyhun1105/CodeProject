using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models
{
    public class Color : BaseEntity
    {
        [StringLength(maximumLength: 30)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
