using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;
using QlyKhachHang.Models;

namespace QlyKhachHang.Controllers
{
    public class CustomerContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerContactController> _logger;

        public CustomerContactController(ApplicationDbContext context, ILogger<CustomerContactController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CustomerContact/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var contacts = await _context.CustomerContacts
                    .Include(c => c.Customer)
                    .OrderByDescending(c => c.CreatedDate)
                    .ToListAsync();

                return View(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer contacts");
                TempData["Error"] = "Có l?i khi t?i danh sách liên h?";
                return View(new List<CustomerContact>());
            }
        }

        // GET: CustomerContact/ByCustomer/5
        public async Task<IActionResult> ByCustomer(int customerId)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    return NotFound();
                }

                var contacts = await _context.CustomerContacts
                    .Where(c => c.CustomerId == customerId)
                    .OrderByDescending(c => c.IsPreferred)
                    .ThenByDescending(c => c.CreatedDate)
                    .ToListAsync();

                ViewData["CustomerId"] = customerId;
                ViewData["CustomerName"] = customer.CustomerName;
                return View(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer contacts");
                TempData["Error"] = "Có l?i khi t?i liên h?";
                return RedirectToAction("Index", "Customer");
            }
        }

        // GET: CustomerContact/Create/5
        public async Task<IActionResult> Create(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var contact = new CustomerContact { CustomerId = customerId };
            ViewData["CustomerId"] = customerId;
            ViewData["CustomerName"] = customer.CustomerName;
            return View(contact);
        }

        // POST: CustomerContact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,ContactType,ContactName,PhoneNumber,EmailAddress,Relationship,IsPreferred")] CustomerContact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Thêm liên h? thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId = contact.CustomerId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating contact");
                    ModelState.AddModelError("", "Có l?i khi thêm liên h?");
                }
            }

            var customer = await _context.Customers.FindAsync(contact.CustomerId);
            ViewData["CustomerId"] = contact.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(contact);
        }

        // GET: CustomerContact/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _context.CustomerContacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(contact.CustomerId);
            ViewData["CustomerId"] = contact.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(contact);
        }

        // POST: CustomerContact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,CustomerId,ContactType,ContactName,PhoneNumber,EmailAddress,Relationship,IsPreferred,CreatedDate")] CustomerContact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contact.ModifiedDate = DateTime.Now;
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "C?p nh?t liên h? thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId = contact.CustomerId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating contact");
                    ModelState.AddModelError("", "Có l?i khi c?p nh?t liên h?");
                }
            }

            var customer = await _context.Customers.FindAsync(contact.CustomerId);
            ViewData["CustomerId"] = contact.CustomerId;
            ViewData["CustomerName"] = customer?.CustomerName;
            return View(contact);
        }

        // POST: CustomerContact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var contact = await _context.CustomerContacts.FindAsync(id);
                if (contact != null)
                {
                    int customerId = contact.CustomerId;
                    _context.CustomerContacts.Remove(contact);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Xóa liên h? thành công";
                    return RedirectToAction(nameof(ByCustomer), new { customerId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting contact");
                TempData["Error"] = "Có l?i khi xóa liên h?";
            }

            return RedirectToAction("Index", "Customer");
        }
    }
}
