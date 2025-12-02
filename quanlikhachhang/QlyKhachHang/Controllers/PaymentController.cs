using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ApplicationDbContext context, ILogger<PaymentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Payment/Index
        public async Task<IActionResult> Index(int? invoiceId = null)
        {
            try
            {
                var payments = _context.Payments
                    .Include(p => p.Invoice)
                    .ThenInclude(i => i.Customer)
                    .AsQueryable();

                if (invoiceId.HasValue)
                {
                    payments = payments.Where(p => p.InvoiceId == invoiceId);
                }

                var paymentList = await payments
                    .OrderByDescending(p => p.PaymentDate)
                    .ToListAsync();

                if (invoiceId.HasValue)
                {
                    ViewData["InvoiceId"] = invoiceId;
                    var invoice = await _context.Invoices.FindAsync(invoiceId);
                    if (invoice != null)
                    {
                        ViewData["InvoiceNumber"] = invoice.InvoiceNumber;
                        ViewData["TotalAmount"] = invoice.TotalAmount;
                        ViewData["PaidAmount"] = invoice.PaidAmount;
                        ViewData["RemainingAmount"] = invoice.TotalAmount - invoice.PaidAmount;
                    }
                }

                return View(paymentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading payments");
                TempData["Error"] = "Có l?i khi t?i danh sách thanh toán";
                return View(new List<Payment>());
            }
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var payment = await _context.Payments
                    .Include(p => p.Invoice)
                    .ThenInclude(i => i.Customer)
                    .FirstOrDefaultAsync(m => m.PaymentId == id);

                if (payment == null)
                {
                    return NotFound();
                }

                return View(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading payment details");
                TempData["Error"] = "Có l?i khi t?i chi ti?t thanh toán";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Payment/Create
        public async Task<IActionResult> Create(int? invoiceId = null)
        {
            try
            {
                var invoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .Where(i => i.Status != "Cancelled")
                    .OrderByDescending(i => i.InvoiceDate)
                    .ToListAsync();

                ViewData["InvoiceId"] = new SelectList(
                    invoices,
                    "InvoiceId",
                    "InvoiceNumber",
                    invoiceId);

                ViewData["PaymentMethods"] = new SelectList(
                    new List<string> { "Ti?n m?t", "Th? tín d?ng", "Chuy?n kho?n ngân hàng", "Ví ?i?n t?" },
                    "Ti?n m?t");

                if (invoiceId.HasValue)
                {
                    var invoice = await _context.Invoices.FindAsync(invoiceId);
                    if (invoice != null)
                    {
                        ViewData["RemainingAmount"] = invoice.TotalAmount - invoice.PaidAmount;
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading create form");
                TempData["Error"] = "Có l?i khi t?i form thanh toán";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,PaymentMethod,Amount,TransactionId,Notes,BankName,BankAccountNumber,BankAccountName")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Validate amount
                    var invoice = await _context.Invoices.FindAsync(payment.InvoiceId);
                    if (invoice == null)
                    {
                        ModelState.AddModelError("InvoiceId", "Hóa ??n không t?n t?i");
                        return View(payment);
                    }

                    decimal remainingAmount = invoice.TotalAmount - invoice.PaidAmount;
                    if (payment.Amount <= 0)
                    {
                        ModelState.AddModelError("Amount", "S? ti?n ph?i l?n h?n 0");
                        return View(payment);
                    }

                    if (payment.Amount > remainingAmount)
                    {
                        ModelState.AddModelError("Amount", $"S? ti?n không ???c v??t quá {remainingAmount:C}");
                        return View(payment);
                    }

                    // Set default status
                    payment.Status = "Pending";
                    payment.CreatedDate = DateTime.Now;

                    _context.Add(payment);

                    // Update invoice paid amount
                    invoice.PaidAmount += payment.Amount;

                    // Check if fully paid
                    if (invoice.PaidAmount >= invoice.TotalAmount)
                    {
                        invoice.Status = "Completed";
                        payment.Status = "Completed";
                        payment.CompletedDate = DateTime.Now;
                    }

                    invoice.ModifiedDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    TempData["Success"] = "T?o b?n ghi thanh toán thành công";
                    return RedirectToAction(nameof(Index), new { invoiceId = payment.InvoiceId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating payment");
                    ModelState.AddModelError("", "Có l?i khi t?o b?n ghi thanh toán");
                }
            }

            var invoices = await _context.Invoices
                .Where(i => i.Status != "Cancelled")
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync();

            ViewData["InvoiceId"] = new SelectList(invoices, "InvoiceId", "InvoiceNumber", payment.InvoiceId);
            ViewData["PaymentMethods"] = new SelectList(
                new List<string> { "Ti?n m?t", "Th? tín d?ng", "Chuy?n kho?n ngân hàng", "Ví ?i?n t?" },
                payment.PaymentMethod);

            return View(payment);
        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var payment = await _context.Payments
                    .Include(p => p.Invoice)
                    .FirstOrDefaultAsync(m => m.PaymentId == id);

                if (payment == null)
                {
                    return NotFound();
                }

                // Only allow editing pending payments
                if (payment.Status != "Pending")
                {
                    TempData["Warning"] = "Ch? có th? s?a các b?n ghi thanh toán ?ang ch? xác nh?n";
                    return RedirectToAction(nameof(Details), new { id = payment.PaymentId });
                }

                ViewData["PaymentMethods"] = new SelectList(
                    new List<string> { "Ti?n m?t", "Th? tín d?ng", "Chuy?n kho?n ngân hàng", "Ví ?i?n t?" },
                    payment.PaymentMethod);

                return View(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading payment for edit");
                TempData["Error"] = "Có l?i khi t?i b?n ghi thanh toán";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Payment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,InvoiceId,PaymentMethod,Amount,TransactionId,Notes,BankName,BankAccountNumber,BankAccountName,CreatedDate")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPayment = await _context.Payments
                        .Include(p => p.Invoice)
                        .FirstOrDefaultAsync(p => p.PaymentId == id);

                    if (existingPayment == null)
                    {
                        return NotFound();
                    }

                    // Check if can edit
                    if (existingPayment.Status != "Pending")
                    {
                        TempData["Warning"] = "Không th? s?a b?n ghi thanh toán ?ã hoàn thành";
                        return RedirectToAction(nameof(Details), new { id });
                    }

                    var invoice = existingPayment.Invoice;
                    decimal amountDifference = payment.Amount - existingPayment.Amount;

                    // Validate new amount
                    decimal remainingAmount = invoice.TotalAmount - (invoice.PaidAmount - existingPayment.Amount);
                    if (amountDifference > remainingAmount && amountDifference > 0)
                    {
                        ModelState.AddModelError("Amount", $"S? ti?n không ???c v??t quá {remainingAmount:C}");
                        return View(payment);
                    }

                    existingPayment.PaymentMethod = payment.PaymentMethod;
                    existingPayment.Amount = payment.Amount;
                    existingPayment.TransactionId = payment.TransactionId;
                    existingPayment.Notes = payment.Notes;
                    existingPayment.BankName = payment.BankName;
                    existingPayment.BankAccountNumber = payment.BankAccountNumber;
                    existingPayment.BankAccountName = payment.BankAccountName;
                    existingPayment.ModifiedDate = DateTime.Now;

                    // Update invoice paid amount
                    invoice.PaidAmount += amountDifference;
                    invoice.ModifiedDate = DateTime.Now;

                    _context.Update(existingPayment);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "C?p nh?t b?n ghi thanh toán thành công";
                    return RedirectToAction(nameof(Details), new { id });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error editing payment");
                    if (!PaymentExists(payment.PaymentId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error editing payment");
                    ModelState.AddModelError("", "Có l?i khi c?p nh?t b?n ghi thanh toán");
                }
            }

            ViewData["PaymentMethods"] = new SelectList(
                new List<string> { "Ti?n m?t", "Th? tín d?ng", "Chuy?n kho?n ngân hàng", "Ví ?i?n t?" },
                payment.PaymentMethod);

            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var payment = await _context.Payments
                    .Include(p => p.Invoice)
                    .ThenInclude(i => i.Customer)
                    .FirstOrDefaultAsync(m => m.PaymentId == id);

                if (payment == null)
                {
                    return NotFound();
                }

                // Only allow deleting pending payments
                if (payment.Status != "Pending")
                {
                    TempData["Warning"] = "Ch? có th? xóa các b?n ghi thanh toán ?ang ch? xác nh?n";
                    return RedirectToAction(nameof(Details), new { id });
                }

                return View(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading payment for delete");
                TempData["Error"] = "Có l?i khi t?i b?n ghi thanh toán";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var payment = await _context.Payments
                    .Include(p => p.Invoice)
                    .FirstOrDefaultAsync(p => p.PaymentId == id);

                if (payment != null)
                {
                    // Only allow deleting pending payments
                    if (payment.Status != "Pending")
                    {
                        TempData["Warning"] = "Không th? xóa b?n ghi thanh toán ?ã hoàn thành";
                        return RedirectToAction(nameof(Index));
                    }

                    // Refund the amount
                    var invoice = payment.Invoice;
                    invoice.PaidAmount -= payment.Amount;
                    invoice.Status = "Pending"; // Reset status to pending
                    invoice.ModifiedDate = DateTime.Now;

                    _context.Payments.Remove(payment);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa b?n ghi thanh toán thành công";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting payment");
                TempData["Error"] = "Có l?i khi xóa b?n ghi thanh toán";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
