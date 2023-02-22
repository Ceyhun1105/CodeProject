using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models
{
    public class Service : BaseEntity
    {
        [StringLength(maximumLength:100)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }
    }
}
