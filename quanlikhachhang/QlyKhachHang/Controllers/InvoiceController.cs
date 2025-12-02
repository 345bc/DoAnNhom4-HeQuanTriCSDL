using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(ApplicationDbContext context, ILogger<InvoiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Invoice/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var invoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceDetails)
                    .ThenInclude(id => id.Product)
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToListAsync();

                return View(invoices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoices");
                TempData["Error"] = "Có lỗi khi tải danh sách hóa đơn";
                return View(new List<Invoice>());
            }
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceDetails)
                    .ThenInclude(id => id.Product)
                    .FirstOrDefaultAsync(m => m.InvoiceId == id);

                if (invoice == null)
                {
                    return NotFound();
                }

                return View(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice details");
                TempData["Error"] = "Có lỗi khi tải chi tiết hóa đơn";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Invoice/Create
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
                TempData["Error"] = "Có lỗi khi tải form tạo hóa đơn";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,InvoiceNumber,InvoiceDate,TotalAmount,Status,Notes")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Generate invoice number if not provided
                    if (string.IsNullOrEmpty(invoice.InvoiceNumber))
                    {
                        invoice.InvoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmmss}";
                    }

                    invoice.CreatedDate = DateTime.Now;
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Tạo hóa đơn thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating invoice");
                    ModelState.AddModelError("", "Có lỗi khi tạo hóa đơn");
                }
            }

            ViewData["CustomerId"] = new SelectList(
                await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                "CustomerId",
                "CustomerName",
                invoice.CustomerId);

            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoice = await _context.Invoices.FindAsync(id);
                if (invoice == null)
                {
                    return NotFound();
                }

                ViewData["CustomerId"] = new SelectList(
                    await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                    "CustomerId",
                    "CustomerName",
                    invoice.CustomerId);

                return View(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice for edit");
                TempData["Error"] = "Có lỗi khi tải hóa đơn";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CustomerId,InvoiceNumber,InvoiceDate,TotalAmount,Status,Notes,CreatedDate")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    invoice.ModifiedDate = DateTime.Now;
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật hóa đơn thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error editing invoice");
                    if (!InvoiceExists(invoice.InvoiceId))
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
                    _logger.LogError(ex, "Error editing invoice");
                    ModelState.AddModelError("", "Có lỗi khi cập nhật hóa đơn");
                }
            }

            ViewData["CustomerId"] = new SelectList(
                await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync(),
                "CustomerId",
                "CustomerName",
                invoice.CustomerId);

            return View(invoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.Customer)
                    .FirstOrDefaultAsync(m => m.InvoiceId == id);

                if (invoice == null)
                {
                    return NotFound();
                }

                return View(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice for delete");
                TempData["Error"] = "Có lỗi khi tải hóa đơn";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var invoice = await _context.Invoices
                    .Include(i => i.InvoiceDetails)
                    .FirstOrDefaultAsync(i => i.InvoiceId == id);

                if (invoice != null)
                {
                    // Delete invoice details first (cascade should handle this, but being explicit)
                    _context.InvoiceDetails.RemoveRange(invoice.InvoiceDetails);
                    _context.Invoices.Remove(invoice);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa hóa đơn thành công";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice");
                TempData["Error"] = "Có lỗi khi xóa hóa đơn";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == id);
        }
    }
}