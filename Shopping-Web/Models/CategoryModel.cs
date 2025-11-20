using System.ComponentModel.DataAnnotations;

namespace Shopping_Web.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage =" Vui lòng nhập tên danh mục")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập mô tả danh mục")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
