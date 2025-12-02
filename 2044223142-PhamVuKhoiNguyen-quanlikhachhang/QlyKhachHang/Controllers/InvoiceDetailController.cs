using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class InvoiceDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InvoiceDetailController> _logger;

        public InvoiceDetailController(ApplicationDbContext context, ILogger<InvoiceDetailController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: InvoiceDetail/Index
        public async Task<IActionResult> Index(int? invoiceId)
        {
            try
            {
                IQueryable<InvoiceDetail> details = _context.InvoiceDetails
                    .Include(id => id.Invoice)
                    .ThenInclude(i => i.Customer)
                    .Include(id => id.Product);

                if (invoiceId.HasValue)
                {
                    details = details.Where(id => id.InvoiceId == invoiceId.Value);
                }

                var items = await details.OrderBy(id => id.InvoiceDetailId).ToListAsync();
                ViewData["InvoiceId"] = invoiceId;
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice details");
                TempData["Error"] = "Có l?i khi t?i danh sách chi ti?t hóa ??n";
                return View(new List<InvoiceDetail>());
            }
        }

        // GET: InvoiceDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoiceDetail = await _context.InvoiceDetails
                    .Include(id => id.Invoice)
                    .ThenInclude(i => i.Customer)
                    .Include(id => id.Product)
                    .FirstOrDefaultAsync(m => m.InvoiceDetailId == id);

                if (invoiceDetail == null)
                {
                    return NotFound();
                }

                return View(invoiceDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice detail");
                TempData["Error"] = "Có l?i khi t?i chi ti?t hóa ??n";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: InvoiceDetail/Create
        public async Task<IActionResult> Create(int? invoiceId)
        {
            try
            {
                ViewData["InvoiceId"] = new SelectList(
                    await _context.Invoices
                        .Include(i => i.Customer)
                        .OrderByDescending(i => i.InvoiceDate)
                        .ToListAsync(),
                    "InvoiceId",
                    "InvoiceNumber");

                ViewData["ProductId"] = new SelectList(
                    await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                    "ProductId",
                    "ProductName");

                if (invoiceId.HasValue)
                {
                    ViewData["InvoiceId"] = new SelectList(
                        await _context.Invoices
                            .Include(i => i.Customer)
                            .OrderByDescending(i => i.InvoiceDate)
                            .ToListAsync(),
                        "InvoiceId",
                        "InvoiceNumber",
                        invoiceId.Value);
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form");
                TempData["Error"] = "Có l?i khi t?i form t?o chi ti?t hóa ??n";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: InvoiceDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,ProductId,Quantity,UnitPrice")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verify invoice exists
                    var invoice = await _context.Invoices.FindAsync(invoiceDetail.InvoiceId);
                    if (invoice == null)
                    {
                        ModelState.AddModelError("InvoiceId", "Hóa ??n không t?n t?i");
                    }
                    else
                    {
                        // Verify product exists
                        var product = await _context.Products.FindAsync(invoiceDetail.ProductId);
                        if (product == null)
                        {
                            ModelState.AddModelError("ProductId", "S?n ph?m không t?n t?i");
                        }
                        else
                        {
                            // Set unit price from product if not provided
                            if (invoiceDetail.UnitPrice == 0)
                            {
                                invoiceDetail.UnitPrice = product.Price;
                            }

                            _context.Add(invoiceDetail);

                            // Update invoice total
                            invoice.TotalAmount += invoiceDetail.LineTotal;
                            _context.Update(invoice);

                            await _context.SaveChangesAsync();
                            TempData["Success"] = "Thêm chi ti?t hóa ??n thành công";
                            return RedirectToAction(nameof(Index), new { invoiceId = invoiceDetail.InvoiceId });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating invoice detail");
                    ModelState.AddModelError("", "Có l?i khi thêm chi ti?t hóa ??n");
                }
            }

            ViewData["InvoiceId"] = new SelectList(
                await _context.Invoices
                    .Include(i => i.Customer)
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToListAsync(),
                "InvoiceId",
                "InvoiceNumber",
                invoiceDetail.InvoiceId);

            ViewData["ProductId"] = new SelectList(
                await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                "ProductId",
                "ProductName",
                invoiceDetail.ProductId);

            return View(invoiceDetail);
        }

        // GET: InvoiceDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
                if (invoiceDetail == null)
                {
                    return NotFound();
                }

                ViewData["InvoiceId"] = new SelectList(
                    await _context.Invoices
                        .Include(i => i.Customer)
                        .OrderByDescending(i => i.InvoiceDate)
                        .ToListAsync(),
                    "InvoiceId",
                    "InvoiceNumber",
                    invoiceDetail.InvoiceId);

                ViewData["ProductId"] = new SelectList(
                    await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                    "ProductId",
                    "ProductName",
                    invoiceDetail.ProductId);

                return View(invoiceDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice detail for edit");
                TempData["Error"] = "Có l?i khi t?i chi ti?t hóa ??n";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: InvoiceDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceDetailId,InvoiceId,ProductId,Quantity,UnitPrice")] InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.InvoiceDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get old invoice detail for total calculation
                    var oldDetail = await _context.InvoiceDetails.AsNoTracking().FirstOrDefaultAsync(id => id.InvoiceDetailId == invoiceDetail.InvoiceDetailId);
                    
                    if (oldDetail != null)
                    {
                        var invoice = await _context.Invoices.FindAsync(invoiceDetail.InvoiceId);
                        if (invoice != null)
                        {
                            // Update invoice total: remove old amount, add new amount
                            invoice.TotalAmount -= oldDetail.LineTotal;
                            invoice.TotalAmount += invoiceDetail.LineTotal;
                            _context.Update(invoice);
                        }
                    }

                    _context.Update(invoiceDetail);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "C?p nh?t chi ti?t hóa ??n thành công";
                    return RedirectToAction(nameof(Index), new { invoiceId = invoiceDetail.InvoiceId });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error editing invoice detail");
                    if (!InvoiceDetailExists(invoiceDetail.InvoiceDetailId))
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
                    _logger.LogError(ex, "Error editing invoice detail");
                    ModelState.AddModelError("", "Có l?i khi c?p nh?t chi ti?t hóa ??n");
                }
            }

            ViewData["InvoiceId"] = new SelectList(
                await _context.Invoices
                    .Include(i => i.Customer)
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToListAsync(),
                "InvoiceId",
                "InvoiceNumber",
                invoiceDetail.InvoiceId);

            ViewData["ProductId"] = new SelectList(
                await _context.Products.OrderBy(p => p.ProductName).ToListAsync(),
                "ProductId",
                "ProductName",
                invoiceDetail.ProductId);

            return View(invoiceDetail);
        }

        // GET: InvoiceDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var invoiceDetail = await _context.InvoiceDetails
                    .Include(id => id.Invoice)
                    .ThenInclude(i => i.Customer)
                    .Include(id => id.Product)
                    .FirstOrDefaultAsync(m => m.InvoiceDetailId == id);

                if (invoiceDetail == null)
                {
                    return NotFound();
                }

                return View(invoiceDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading invoice detail for delete");
                TempData["Error"] = "Có l?i khi t?i chi ti?t hóa ??n";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: InvoiceDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
                if (invoiceDetail != null)
                {
                    int invoiceId = invoiceDetail.InvoiceId;

                    // Update invoice total
                    var invoice = await _context.Invoices.FindAsync(invoiceId);
                    if (invoice != null)
                    {
                        invoice.TotalAmount -= invoiceDetail.LineTotal;
                        _context.Update(invoice);
                    }

                    _context.InvoiceDetails.Remove(invoiceDetail);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa chi ti?t hóa ??n thành công";
                    return RedirectToAction(nameof(Index), new { invoiceId = invoiceId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting invoice detail");
                TempData["Error"] = "Có l?i khi xóa chi ti?t hóa ??n";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(int id)
        {
            return _context.InvoiceDetails.Any(e => e.InvoiceDetailId == id);
        }
    }
}
