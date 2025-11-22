using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập email")]
        public string Email { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage ="Vui lòng nhập Password")]
        public string Password { get; set; }
       
      
    }
}
