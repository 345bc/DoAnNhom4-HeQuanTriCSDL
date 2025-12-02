using System.Security.Cryptography;
using System.Text;
using QlyKhachHang.Data;
using QlyKhachHang.Models;
using Microsoft.EntityFrameworkCore;

namespace QlyKhachHang.Services
{
    public interface IAuthenticationService
    {
        Task<Customer?> LoginAsync(string usernameOrEmail, string password);
        Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model);
        Task<Customer?> GetCustomerByUsernameAsync(string username);
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<bool> UpdateLastLoginAsync(int customerId);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ApplicationDbContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Customer?> LoginAsync(string usernameOrEmail, string password)
        {
            try
            {
                // Add timeout to prevent hanging queries
                using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    var customer = await _context.Customers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => 
                            (c.Username == usernameOrEmail || c.Email == usernameOrEmail) &&
                            c.Status == "Active", cts.Token);

                    if (customer == null)
                    {
                        _logger.LogWarning($"Login attempt failed: user not found for {usernameOrEmail}");
                        return null;
                    }

                    // ✅ Use BCrypt for password verification
                    if (!VerifyPassword(password, customer.PasswordHash))
                    {
                        _logger.LogWarning($"Login attempt failed: incorrect password for {usernameOrEmail}");
                        return null;
                    }

                    return customer;
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogError($"Login query timeout for {usernameOrEmail}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login process");
                return null;
            }
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(10)))
                {
                    // Kiểm tra username đã tồn tại
                    if (await _context.Customers.AnyAsync(c => c.Username == model.Username, cts.Token))
                    {
                        return (false, "Tên đăng nhập đã tồn tại");
                    }

                    // Kiểm tra email đã tồn tại
                    if (await _context.Customers.AnyAsync(c => c.Email == model.Email, cts.Token))
                    {
                        return (false, "Email đã được sử dụng");
                    }

                    var customer = new Customer
                    {
                        CustomerName = model.CustomerName,
                        Username = model.Username,
                        Email = model.Email,
                        Phone = model.PhoneNumber ?? string.Empty,
                        Address = model.Address ?? string.Empty,
                        City = model.City ?? string.Empty,
                        PostalCode = model.PostalCode ?? string.Empty,
                        PasswordHash = HashPassword(model.Password),  // ✅ Hash with BCrypt
                        Status = "Active",
                        CreatedDate = DateTime.Now
                    };

                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync(cts.Token);

                    _logger.LogInformation($"New customer registered: {model.Username}");
                    return (true, "Đăng ký thành công");
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogError($"Registration query timeout for {model.Username}");
                return (false, "Hết thời gian chờ, vui lòng thử lại");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return (false, "Có lỗi khi đăng ký");
            }
        }

        public async Task<Customer?> GetCustomerByUsernameAsync(string username)
        {
            using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(10)))
            {
                return await _context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Username == username && c.Status == "Active", cts.Token);
            }
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(10)))
            {
                return await _context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Email == email && c.Status == "Active", cts.Token);
            }
        }

        public async Task<bool> UpdateLastLoginAsync(int customerId)
        {
            try
            {
                using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(5)))
                {
                    // Fire and forget - don't wait for this to complete
                    // Update last login in background
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            var customer = await _context.Customers.FindAsync(
                                new object[] { customerId }, 
                                cancellationToken: cts.Token);
                            
                            if (customer != null)
                            {
                                customer.LastLoginDate = DateTime.Now;
                                _context.Update(customer);
                                await _context.SaveChangesAsync(cts.Token);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error updating last login in background");
                        }
                    }, cts.Token);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating last login");
                return false;
            }
        }

        /// <summary>
        /// Hash password using BCrypt (compatible with existing database)
        /// </summary>
        public string HashPassword(string password)
        {
            // ✅ Use BCrypt.Net-Next for password hashing
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 11);
        }

        /// <summary>
        /// Verify password using BCrypt (compatible with existing database)
        /// </summary>
        public bool VerifyPassword(string password, string hash)
        {
            try
            {
                // ✅ Use BCrypt.Net-Next to verify password
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying password");
                return false;
            }
        }
    }
}
