using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Data;

namespace QlyKhachHang.Controllers
{
    public class CustomerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerDashboardController> _logger;

        public CustomerDashboardController(ApplicationDbContext context, ILogger<CustomerDashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: CustomerDashboard/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var dashboard = new CustomerDashboardViewModel
                {
                    TotalCustomers = await _context.Customers.CountAsync(),
                    ActiveCustomers = await _context.Customers.CountAsync(c => c.Status == "Active"),
                    InactiveCustomers = await _context.Customers.CountAsync(c => c.Status == "Inactive"),
                    BlockedCustomers = await _context.Customers.CountAsync(c => c.Status == "Blocked"),
                    
                    TotalInvoices = await _context.Invoices.CountAsync(),
                    PaidInvoices = await _context.Invoices.CountAsync(i => i.Status == "Paid" || i.Status == "Completed"),
                    PendingInvoices = await _context.Invoices.CountAsync(i => i.Status == "Pending"),
                    
                    TotalReviews = await _context.Reviews.CountAsync(),
                    AverageRating = await GetAverageRatingAsync(),
                    
                    TotalRevenue = await _context.Invoices
                        .Where(i => i.Status == "Paid" || i.Status == "Completed")
                        .SumAsync(i => (decimal?)i.TotalAmount) ?? 0,
                    
                    NewCustomersThisMonth = await _context.Customers
                        .CountAsync(c => c.CreatedDate.Month == DateTime.Now.Month && c.CreatedDate.Year == DateTime.Now.Year),
                    
                    RecentCustomers = await _context.Customers
                        .OrderByDescending(c => c.CreatedDate)
                        .Take(5)
                        .ToListAsync(),
                    
                    TopValuedCustomers = await GetTopValuedCustomersAsync(),
                    
                    MostActiveReviewers = await GetMostActiveReviewersAsync()
                };

                return View(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading customer dashboard");
                TempData["Error"] = "Có l?i khi t?i dashboard. Vui lòng th? l?i sau.";
                
                // Return empty dashboard instead of redirecting
                var emptyDashboard = new CustomerDashboardViewModel();
                return View(emptyDashboard);
            }
        }

        private async Task<decimal> GetAverageRatingAsync()
        {
            try
            {
                var reviews = await _context.Reviews.ToListAsync();
                if (reviews.Count == 0) return 0;
                return (decimal)reviews.Average(r => r.Rating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting average rating");
                return 0;
            }
        }

        private async Task<List<dynamic>> GetTopValuedCustomersAsync()
        {
            try
            {
                var topCustomers = await _context.Customers
                    .Join(_context.Invoices, c => c.CustomerId, i => i.CustomerId, (c, i) => new { c, i })
                    .Where(x => x.i.Status == "Paid" || x.i.Status == "Completed")
                    .GroupBy(x => x.c)
                    .Select(g => new
                    {
                        Customer = g.Key,
                        TotalSpent = g.Sum(x => x.i.TotalAmount),
                        OrderCount = g.Count()
                    })
                    .OrderByDescending(x => x.TotalSpent)
                    .Take(5)
                    .ToListAsync();

                return topCustomers.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top valued customers");
                return new List<dynamic>();
            }
        }

        private async Task<List<dynamic>> GetMostActiveReviewersAsync()
        {
            try
            {
                var topReviewers = await _context.Customers
                    .Join(_context.Reviews, c => c.CustomerId, r => r.CustomerId, (c, r) => new { c, r })
                    .GroupBy(x => x.c)
                    .Select(g => new
                    {
                        Customer = g.Key,
                        ReviewCount = g.Count(),
                        AverageRating = g.Average(x => (double)x.r.Rating)
                    })
                    .OrderByDescending(x => x.ReviewCount)
                    .Take(5)
                    .ToListAsync();

                return topReviewers.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting most active reviewers");
                return new List<dynamic>();
            }
        }
    }

    /// <summary>
    /// View Model cho Dashboard Khách Hàng
    /// </summary>
    public class CustomerDashboardViewModel
    {
        // Th?ng kê c? b?n
        public int TotalCustomers { get; set; }
        public int ActiveCustomers { get; set; }
        public int InactiveCustomers { get; set; }
        public int BlockedCustomers { get; set; }

        // Th?ng kê Hóa ??n
        public int TotalInvoices { get; set; }
        public int PaidInvoices { get; set; }
        public int PendingInvoices { get; set; }

        // Th?ng kê ?ánh Giá
        public int TotalReviews { get; set; }
        public decimal AverageRating { get; set; }

        // Th?ng kê Doanh Thu
        public decimal TotalRevenue { get; set; }
        public int NewCustomersThisMonth { get; set; }

        // Danh sách chi ti?t
        public List<Models.Customer> RecentCustomers { get; set; } = new();
        public List<dynamic> TopValuedCustomers { get; set; } = new();
        public List<dynamic> MostActiveReviewers { get; set; } = new();
    }
}
