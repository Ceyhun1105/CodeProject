using MultiShopMvc.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Models
{
    public class Setting : BaseEntity
    {
        [StringLength(maximumLength: 100)]
        public string? Key { get; set; }
        [StringLength(maximumLength: 1000)]
        public string Value { get; set; }
    }
}
