using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class CustomerNoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerNoteController> _logger;

        public CustomerNoteController(ApplicationDbContext context, ILogger<CustomerNoteController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CustomerNote/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var notes = await _context.CustomerNotes
                    .Include(n => n.Customer)
                    .OrderByDescending(n => n.CreatedDate)
                    .ToListAsync();

                return View(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer notes");
                TempData["Error"] = "Có l?i khi t?i danh sách ghi chú";
                return View(new List<CustomerNote>());
            }
        }

        // GET: CustomerNote/ByCustomer/5
        public async Task<IActionResult> ByCustomer(int customerId)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return NotFound();
                }

                var notes = await _context.CustomerNotes
                    .Where(n => n.CustomerId == customerId)
                    .OrderByDescending(n => n.CreatedDate)
                    .ToListAsync();

                ViewData["CustomerId"] = customerId;
                ViewData["CustomerName"] = customer.CustomerName;
                return View(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer notes");
                TempData["Error"] = "Có l?i khi t?i ghi chú";
                return RedirectToAction("Index", "Customer");
            }
        }

        // GET: CustomerNote/Create/5
        public async Task<IActionResult> Create(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var note = new CustomerNote { CustomerId = customerId };
            ViewData["CustomerId"] = customerId;
            ViewData["CustomerName"] = customer.CustomerName;
            return View(note);
        }

        // POST: CustomerNote/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,NoteContent,NoteType,Priority,CreatedBy")] CustomerNote note)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    note.CreatedDate = DateTime.Now;
                    _context.Add(note);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm ghi chú thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId = note.CustomerId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating note");
                    ModelState.AddModelError("", "Có l?i khi thêm ghi chú");
                }
            }

            var customer = await _context.Customers.FindAsync(note.CustomerId);
            ViewData["CustomerId"] = note.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(note);
        }

        // GET: CustomerNote/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _context.CustomerNotes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(note.CustomerId);
            ViewData["CustomerId"] = note.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(note);
        }

        // POST: CustomerNote/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,CustomerId,NoteContent,NoteType,Priority,CreatedDate,CreatedBy")] CustomerNote note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    note.ModifiedDate = DateTime.Now;
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "C?p nh?t ghi chú thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId = note.CustomerId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating note");
                    ModelState.AddModelError("", "Có l?i khi c?p nh?t ghi chú");
                }
            }

            var customer = await _context.Customers.FindAsync(note.CustomerId);
            ViewData["CustomerId"] = note.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(note);
        }

        // POST: CustomerNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var note = await _context.CustomerNotes.FindAsync(id);
                if (note != null)
                {
                    int customerId = note.CustomerId;
                    _context.CustomerNotes.Remove(note);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa ghi chú thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting note");
                TempData["Error"] = "Có l?i khi xóa ghi chú";
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
