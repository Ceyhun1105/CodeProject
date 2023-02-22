using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models
{
    public class Size : BaseEntity
    {
        [StringLength(maximumLength: 10)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }

    }

}
