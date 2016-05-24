using System.ComponentModel.DataAnnotations;

namespace SimpleBookKeeping.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login field is required")]
        [StringLength(30, ErrorMessage = "Login is to big")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [StringLength(30, ErrorMessage = "Password is to big")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}