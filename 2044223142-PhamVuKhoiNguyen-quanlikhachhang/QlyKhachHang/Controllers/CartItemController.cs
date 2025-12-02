using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartItemController> _logger;

        public CartItemController(ApplicationDbContext context, ILogger<CartItemController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CartItem/Index
        public async Task<IActionResult> Index(int? cartId)
        {
            try
            {
                IQueryable<CartItem> cartItems = _context.CartItems
                    .Include(ci => ci.Cart)
                    .ThenInclude(c => c.Customer)
                    .Include(ci => ci.Product);

                if (cartId.HasValue)
                {
                    cartItems = cartItems.Where(ci => ci.CartId == cartId.Value);
                }

                var items = await cartItems.OrderByDescending(ci => ci.AddedDate).ToListAsync();
                ViewData["CartId"] = cartId;
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart items");
                TempData["Error"] = "Có lỗi khi tải danh sách mặt hàng";
                return View(new List<CartItem>());
            }
        }

        // GET: CartItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cartItem = await _context.CartItems
                    .Include(ci => ci.Cart)
                    .ThenInclude(c => c.Customer)
                    .Include(ci => ci.Product)
                    .FirstOrDefaultAsync(m => m.CartItemId == id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                return View(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart item details");
                TempData["Error"] = "Có lỗi khi tải chi tiết mặt hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: CartItem/Create
        public async Task<IActionResult> Create(int? cartId)
        {
            try
            {
                ViewData["CartId"] = new SelectList(
                    await _context.Carts
                        .Include(c => c.Customer)
                        .OrderByDescending(c => c.CreatedDate)
                        .ToListAsync(),
                    "CartId",
                    "CartId");

                ViewData["ProductId"] = new SelectList(
                    await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                    "ProductId",
                    "ProductName");

                if (cartId.HasValue)
                {
                    ViewData["CartId"] = new SelectList(
                        await _context.Carts
                            .Include(c => c.Customer)
                            .OrderByDescending(c => c.CreatedDate)
                            .ToListAsync(),
                        "CartId",
                        "CartId",
                        cartId.Value);
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form");
                TempData["Error"] = "Có lỗi khi tải form tạo mặt hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CartItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,ProductId,Quantity,UnitPrice")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verify cart exists
                    var cart = await _context.Carts.FindAsync(cartItem.CartId);
                    if (cart == null)
                    {
                        ModelState.AddModelError("CartId", "Giỏ hàng không tồn tại");
                    }
                    else
                    {
                        // Verify product exists
                        var product = await _context.Products.FindAsync(cartItem.ProductId);
                        if (product == null)
                        {
                            ModelState.AddModelError("ProductId", "Sản phẩm không tồn tại");
                        }
                        else if (product.Stock < cartItem.Quantity)
                        {
                            ModelState.AddModelError("Quantity", "Số lượng vượt quá tồn kho");
                        }
                        else
                        {
                            // Set unit price from product if not provided
                            if (cartItem.UnitPrice == 0)
                            {
                                cartItem.UnitPrice = product.Price;
                            }

                            cartItem.AddedDate = DateTime.Now;
                            _context.Add(cartItem);
                            await _context.SaveChangesAsync();
                            TempData["Success"] = "Thêm mặt hàng vào giỏ hàng thành công";
                            return RedirectToAction(nameof(Index), new { cartId = cartItem.CartId });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating cart item");
                    ModelState.AddModelError("", "Có lỗi khi thêm mặt hàng");
                }
            }

            ViewData["CartId"] = new SelectList(
                await _context.Carts
                    .Include(c => c.Customer)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync(),
                "CartId",
                "CartId",
                cartItem.CartId);

            ViewData["ProductId"] = new SelectList(
                await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                "ProductId",
                "ProductName",
                cartItem.ProductId);

            return View(cartItem);
        }

        // GET: CartItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                ViewData["CartId"] = new SelectList(
                    await _context.Carts
                        .Include(c => c.Customer)
                        .OrderByDescending(c => c.CreatedDate)
                        .ToListAsync(),
                    "CartId",
                    "CartId",
                    cartItem.CartId);

                ViewData["ProductId"] = new SelectList(
                    await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                    "ProductId",
                    "ProductName",
                    cartItem.ProductId);

                return View(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart item for edit");
                TempData["Error"] = "Có lỗi khi tải mặt hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CartItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,CartId,ProductId,Quantity,UnitPrice,AddedDate")] CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verify product stock
                    var product = await _context.Products.FindAsync(cartItem.ProductId);
                    if (product != null && product.Stock < cartItem.Quantity)
                    {
                        ModelState.AddModelError("Quantity", "Số lượng vượt quá tồn kho");
                    }
                    else
                    {
                        _context.Update(cartItem);
                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Cập nhật mặt hàng thành công";
                        return RedirectToAction(nameof(Index), new { cartId = cartItem.CartId });
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error editing cart item");
                    if (!CartItemExists(cartItem.CartItemId))
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
                    _logger.LogError(ex, "Error editing cart item");
                    ModelState.AddModelError("", "Có lỗi khi cập nhật mặt hàng");
                }
            }

            ViewData["CartId"] = new SelectList(
                await _context.Carts
                    .Include(c => c.Customer)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync(),
                "CartId",
                "CartId",
                cartItem.CartId);

            ViewData["ProductId"] = new SelectList(
                await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                "ProductId",
                "ProductName",
                cartItem.ProductId);

            return View(cartItem);
        }

        // GET: CartItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cartItem = await _context.CartItems
                    .Include(ci => ci.Cart)
                    .ThenInclude(c => c.Customer)
                    .Include(ci => ci.Product)
                    .FirstOrDefaultAsync(m => m.CartItemId == id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                return View(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cart item for delete");
                TempData["Error"] = "Có lỗi khi tải mặt hàng";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(id);
                if (cartItem != null)
                {
                    int cartId = cartItem.CartId;
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa mặt hàng khỏi giỏ hàng thành công";
                    return RedirectToAction(nameof(Index), new { cartId = cartId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting cart item");
                TempData["Error"] = "Có lỗi khi xóa mặt hàng";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.CartItemId == id);
        }
    }
}