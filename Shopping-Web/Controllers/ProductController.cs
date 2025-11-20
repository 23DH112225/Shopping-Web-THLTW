using Microsoft.AspNetCore.Mvc;
using Shopping_Web.Repository;
using Microsoft.EntityFrameworkCore; // Cần thêm namespace này để dùng .Include() và .FirstOrDefaultAsync()

namespace Shopping_Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext context)
        {
            _dataContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            // 1. Kiểm tra ID: Nếu ID là 0 hoặc không hợp lệ, chuyển hướng hoặc trả về 404.
            // (Trong trường hợp này, tham số là 'int', nên nó không thể là null, 
            // nhưng nên kiểm tra giá trị hợp lệ, ví dụ: Id <= 0)
            if (Id <= 0)
            {
                return RedirectToAction("Index");
            }

            // 2. Truy vấn Dữ liệu (Sử dụng Async và Include)
            var productById = await _dataContext.Products
                .Include(p => p.Brand) // **QUAN TRỌNG:** Tải Brand để @Model.Brand.Name không bị NULL
                .FirstOrDefaultAsync(p => p.Id == Id);

            // 3. Xử lý NULL (Kiểm tra xem có tìm thấy sản phẩm không)
            if (productById == null)
            {
                // Nếu không tìm thấy sản phẩm, trả về trang lỗi 404
                return NotFound();
            }

            // 4. Truyền Model đến View
            return View(productById);
        }
    }
}