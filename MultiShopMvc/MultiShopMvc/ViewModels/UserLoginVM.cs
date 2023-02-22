using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.ViewModels
{
    public class UserLoginVM
    {
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 25,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
