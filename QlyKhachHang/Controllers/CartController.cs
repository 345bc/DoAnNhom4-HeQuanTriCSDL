using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Cart/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var carts = await _context.Carts
                    .Include(c => c.Customer)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync();

                return View(carts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading carts");
                TempData["Error"] = "Có lỗi khi tải danh sách giỏ hàng";
                return View(new List<Cart>());
            }
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Customer)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(m => m.CartId == id);

                if (cart == null)
                {
                    return NotFound();
                }

                return View(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart details");
                TempData["Error"] = "Có lỗi khi tải chi tiết giỏ hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Cart/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewData["CustomerId"] = new SelectList(
                    await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                    "CustomerId",
                    "CustomerName");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form");
                TempData["Error"] = "Có lỗi khi tải form tạo giỏ hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cart/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cart.CreatedDate = DateTime.Now;
                    _context.Add(cart);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Tạo giỏ hàng thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating cart");
                    ModelState.AddModelError("", "Có lỗi khi tạo giỏ hàng");
                }
            }

            ViewData["CustomerId"] = new SelectList(
                await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                "CustomerId",
                "CustomerName",
                cart.CustomerId);
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cart = await _context.Carts.FindAsync(id);
                if (cart == null)
                {
                    return NotFound();
                }

                ViewData["CustomerId"] = new SelectList(
                    await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                    "CustomerId",
                    "CustomerName",
                    cart.CustomerId);
                return View(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart for edit");
                TempData["Error"] = "Có lỗi khi tải giỏ hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,CustomerId,CreatedDate")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cart.ModifiedDate = DateTime.Now;
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật giỏ hàng thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error editing cart");
                    if (!CartExists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error editing cart");
                    ModelState.AddModelError("", "Có lỗi khi cập nhật giỏ hàng");
                }
            }

            ViewData["CustomerId"] = new SelectList(
                await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                "CustomerId",
                "CustomerName",
                cart.CustomerId);
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Customer)
                    .FirstOrDefaultAsync(m => m.CartId == id);

                if (cart == null)
                {
                    return NotFound();
                }

                return View(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart for delete");
                TempData["Error"] = "Có lỗi khi tải giỏ hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.CartId == id);

                if (cart != null)
                {
                    // Delete cart items first (cascade should handle this, but being explicit)
                    _context.CartItems.RemoveRange(cart.CartItems);
                    _context.Carts.Remove(cart);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa giỏ hàng thành công";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting cart");
                TempData["Error"] = "Có lỗi khi xóa giỏ hàng";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/Checkout/5
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Customer)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(m => m.CartId == id);

                if (cart == null)
                {
                    return NotFound();
                }

                // Kiểm tra giỏ hàng có sản phẩm không
                if (!cart.CartItems.Any())
                {
                    TempData["Warning"] = "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.";
                    return RedirectToAction(nameof(Details), new { id = cart.CartId });
                }

                // Tính tổng tiền
                ViewBag.SubTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
                ViewBag.ShippingFee = 30000m; // Phí vận chuyển cố định
                ViewBag.TotalAmount = ViewBag.SubTotal + ViewBag.ShippingFee;

                // Danh sách phương thức thanh toán
                ViewBag.PaymentMethods = new SelectList(new[]
                {
                    new { Value = "Cash", Text = "Tiền Mặt" },
                    new { Value = "CreditCard", Text = "Thẻ Tín Dụng" },
                    new { Value = "BankTransfer", Text = "Chuyển Khoản Ngân Hàng" },
                    new { Value = "MobilePayment", Text = "Ví Điện Tử (Momo/ZaloPay)" }
                }, "Value", "Text");

                return View(cart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading checkout page");
                TempData["Error"] = "Có lỗi khi tải trang thanh toán";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Cart/ProcessCheckout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(int cartId, string paymentMethod, string shippingAddress, string notes)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // 1. Lấy thông tin giỏ hàng
                var cart = await _context.Carts
                    .Include(c => c.Customer)
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.CartId == cartId);

                if (cart == null || !cart.CartItems.Any())
                {
                    TempData["Error"] = "Giỏ hàng không hợp lệ hoặc trống";
                    return RedirectToAction(nameof(Index));
                }

                // 2. Tính toán
                var subTotal = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
                var shippingFee = 30000m;
                var totalAmount = subTotal + shippingFee;

                // 3. Tạo Invoice (Hóa Đơn)
                var invoice = new Invoice
                {
                    CustomerId = cart.CustomerId,
                    InvoiceNumber = GenerateInvoiceNumber(),
                    InvoiceDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    PaidAmount = 0, // Chưa thanh toán
                    Status = "Pending", // Chờ xác nhận
                    Notes = string.IsNullOrEmpty(notes) ? $"Đơn hàng từ giỏ hàng #{cartId}" : notes,
                    CreatedDate = DateTime.Now
                };

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync(); // Lưu để có InvoiceId

                // 4. Tạo InvoiceDetails (Chi Tiết Hóa Đơn)
                foreach (var cartItem in cart.CartItems)
                {
                    var invoiceDetail = new InvoiceDetail
                    {
                        InvoiceId = invoice.InvoiceId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice
                    };

                    _context.InvoiceDetails.Add(invoiceDetail);

                    // Cập nhật tồn kho
                    var product = await _context.Products.FindAsync(cartItem.ProductId);
                    if (product != null)
                    {
                        product.Stock -= cartItem.Quantity;
                        _context.Products.Update(product);
                    }
                }

                // 5. Tạo Payment Record (Ban đầu)
                var payment = new Payment
                {
                    InvoiceId = invoice.InvoiceId,
                    Amount = 0, // Chưa thanh toán
                    PaymentMethod = paymentMethod,
                    Status = "Pending",
                    PaymentDate = DateTime.Now,
                    Notes = $"Phương thức thanh toán: {GetPaymentMethodText(paymentMethod)}",
                    CreatedDate = DateTime.Now
                };

                _context.Payments.Add(payment);

                // 6. Xóa giỏ hàng sau khi đặt hàng thành công
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Carts.Remove(cart);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["Success"] = $"Đặt hàng thành công! Mã hóa đơn: {invoice.InvoiceNumber}. Tổng tiền: {totalAmount:N0} VNĐ";
                
                // Chuyển đến trang chi tiết hóa đơn
                return RedirectToAction("Details", "Invoice", new { id = invoice.InvoiceId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error processing checkout");
                TempData["Error"] = "Có lỗi khi xử lý đơn hàng. Vui lòng thử lại.";
                return RedirectToAction(nameof(Checkout), new { id = cartId });
            }
        }

        // Helper: Tạo mã hóa đơn tự động
        private string GenerateInvoiceNumber()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"INV{timestamp}{random}";
        }

        // Helper: Lấy tên phương thức thanh toán
        private string GetPaymentMethodText(string paymentMethod)
        {
            return paymentMethod switch
            {
                "Cash" => "Tiền Mặt",
                "CreditCard" => "Thẻ Tín Dụng",
                "BankTransfer" => "Chuyển Khoản Ngân Hàng",
                "MobilePayment" => "Ví Điện Tử",
                _ => "Khác"
            };
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}