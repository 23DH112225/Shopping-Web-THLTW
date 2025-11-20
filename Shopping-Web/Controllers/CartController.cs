using Microsoft.AspNetCore.Mvc;
using Shopping_Web.Models;
using Shopping_Web.Models.ViewModels;
using Shopping_Web.Repository;

namespace Shopping_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        public CartController(DataContext _context)
        {
            _dataContext = _context;
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price),
            };
            return View(cartVM);
        }
        public IActionResult Checkout()
        {
            return View("~/Views/Checkout/Index.cshtml");
        }
        public async Task<IActionResult> Add(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if (cartItems == null)
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItems.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["success"] = "Sản Phẩm Đã Được Thêm Vào Giỏ Hàng! ";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == Id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["success"] = "Đã giảm số lượng sản phẩm thành công! ";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int Id)
        {
            // 1. Lấy giỏ hàng từ Session
            // Nếu cart là null, nó sẽ ném lỗi NullReferenceException khi gọi .Where().
            // Dù bạn dùng GetJson, tốt nhất nên đảm bảo nó không null (Giả định SessionExtensions hoạt động)
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

            // **Bổ sung kiểm tra an toàn:** Nếu giỏ hàng không có gì (dù nó không nên xảy ra ở đây)
            if (cart == null)
            {
                TempData["error"] = "Giỏ hàng trống.";
                return RedirectToAction("Index");
            }

            // 2. Tìm sản phẩm cần tăng số lượng
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            // 3. Xử lý logic Tăng số lượng
            if (cartItem != null)
            {
                // **KHẮC PHỤC CHÍNH Ở ĐÂY: LUÔN TĂNG SỐ LƯỢNG.**
                // Không cần kiểm tra Quantity > 1, chỉ cần tăng lên.
                cartItem.Quantity += 1;

                // 4. Lưu lại Session
                HttpContext.Session.SetJson("Cart", cart);
                TempData["success"] = "Đã tăng số lượng sản phẩm thành công!";
            }
            else
            {
                // Tùy chọn: Nếu sản phẩm không tìm thấy trong giỏ hàng, thông báo lỗi.
                TempData["error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
            }

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int Id) // Không cần async/await nếu không gọi DB
        {
            // 1. Lấy giỏ hàng an toàn
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            // **LỖI LOGIC TRONG VIEW CẦN CHÚ Ý:** // Trong View, bạn đang sử dụng asp-action="Decrease" cho nút xóa "Xóa"
            // Bạn nên sửa nút Xóa trong View để gọi asp-action="Remove"

            // 2. Xóa tất cả các mặt hàng có ProductId tương ứng
            cart.RemoveAll(p => p.ProductId == Id);

            // 3. Cập nhật Session
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart"); // Xóa hẳn khóa nếu giỏ hàng trống
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart); // Cập nhật giỏ hàng còn lại
            }

            TempData["success"] = "Đã xóa sản phẩm khỏi giỏ hàng thành công!";
            return RedirectToAction("Index");
        }
        public IActionResult Clear()
        {
            // 1. Xóa khóa "Cart" khỏi Session
            HttpContext.Session.Remove("Cart");

            TempData["success"] = "Đã xóa toàn bộ giỏ hàng thành công!";
            return RedirectToAction("Index");
        }


    }

}

