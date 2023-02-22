using System.ComponentModel.DataAnnotations;

namespace MultiShopMvc.ViewModels
{
    public class UserRegisterVM
    {
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 100)]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 8), DataType(DataType.Password)]
        [Compare("Password")]
        public string RetryPassword { get; set; }

    }
}
