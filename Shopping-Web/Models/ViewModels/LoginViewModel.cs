using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; }
     
        [DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
