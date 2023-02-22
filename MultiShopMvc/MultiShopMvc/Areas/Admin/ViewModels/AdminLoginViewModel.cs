using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.Areas.Admin.ViewModels
{
    public class AdminLoginViewModel
    {
        [StringLength(maximumLength:50)]
        public string UserName { get; set; }
        [StringLength(maximumLength:25,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
