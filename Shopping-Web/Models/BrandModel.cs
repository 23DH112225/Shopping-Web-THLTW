using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = " Vui lòng nhập tên Hãng")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = " Vui lòng nhập mô tả Hãng")]
        public string Description { get; set; }
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
