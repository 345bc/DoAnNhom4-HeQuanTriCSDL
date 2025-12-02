using Microsoft.EntityFrameworkCore;
using QlyKhachHang.Models;

namespace QlyKhachHang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!; 
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<CustomerNote> CustomerNotes { get; set; } = null!;
        public DbSet<CustomerContact> CustomerContacts { get; set; } = null!;
        public DbSet<CustomerActivity> CustomerActivities { get; set; } = null!;
        public DbSet<CustomerSegment> CustomerSegments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User relationships
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure Customer relationships and constraints
            var customerEntity = modelBuilder.Entity<Customer>();
            
            // Index configurations
            customerEntity
                .HasIndex(c => c.Email)
                .IsUnique();

            customerEntity
                .HasIndex(c => c.Username)
                .IsUnique();

            // Relationship with User
            customerEntity
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            customerEntity
                .HasMany(c => c.Carts)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            customerEntity
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            customerEntity
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Product relationships
            var productEntity = modelBuilder.Entity<Product>();
            productEntity.Property(p => p.ProductId).HasColumnName("Product_Id");
            
            productEntity
                .HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            productEntity
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            productEntity
                .HasMany(p => p.InvoiceDetails)
                .WithOne(id => id.Product)
                .HasForeignKey(id => id.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Cart relationships
            var cartEntity = modelBuilder.Entity<Cart>();
            cartEntity.Property(c => c.CartId).HasColumnName("Cart_Id");
            cartEntity.Property(c => c.CustomerId).HasColumnName("MaKH");
            
            cartEntity
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Invoice relationships
            var invoiceEntity = modelBuilder.Entity<Invoice>();
            invoiceEntity.Property(i => i.InvoiceId).HasColumnName("Order_Id");
            invoiceEntity.Property(i => i.CustomerId).HasColumnName("MaKH");
            
            invoiceEntity
                .HasMany(i => i.InvoiceDetails)
                .WithOne(id => id.Invoice)
                .HasForeignKey(id => id.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Invoice to Payment relationships
            invoiceEntity
                .HasMany(i => i.Payments)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure InvoiceDetail
            var invoiceDetailEntity = modelBuilder.Entity<InvoiceDetail>();
            invoiceDetailEntity.Property(id => id.InvoiceDetailId).HasColumnName("OrderDetail_Id");
            invoiceDetailEntity.Property(id => id.InvoiceId).HasColumnName("Order_Id");
            invoiceDetailEntity.Property(id => id.ProductId).HasColumnName("Product_Id");

            // Configure Review
            var reviewEntity = modelBuilder.Entity<Review>();
            reviewEntity.Property(r => r.ReviewId).HasColumnName("Review_Id");
            reviewEntity.Property(r => r.CustomerId).HasColumnName("MaKH");
            reviewEntity.Property(r => r.ProductId).HasColumnName("Product_Id");

            // Configure column types and precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TotalAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.PaidAmount)
                .HasPrecision(12, 2);

            modelBuilder.Entity<InvoiceDetail>()
                .Property(id => id.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(12, 2);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Helper function để hash password
            string HashPassword(string password)
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }

            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    UserID = 1, 
                    Name = "Admin", 
                    Username = "admin", 
                    Email = "admin@shop.com", 
                    Password = HashPassword("123456"),  // ✅ Hash password
                    Role = "Admin", 
                    CreatedAt = new DateTime(2025, 11, 24, 14, 10, 19) 
                },
                new User 
                { 
                    UserID = 2, 
                    Name = "Nhân Viên", 
                    Username = "nhanvien", 
                    Email = "staff@shop.com", 
                    Password = HashPassword("123456"),  // ✅ Hash password
                    Role = "Employee", 
                    CreatedAt = new DateTime(2025, 11, 24, 14, 10, 19) 
                },
                new User 
                { 
                    UserID = 3, 
                    Name = "Khách Hàng 1", 
                    Username = "khachhang1", 
                    Email = "kh1@gmail.com", 
                    Password = HashPassword("MatKhauMoi_123"),  // ✅ Hash password
                    Role = "Customer", 
                    CreatedAt = new DateTime(2025, 11, 24, 14, 10, 19) 
                },
                new User 
                { 
                    UserID = 4, 
                    Name = "Khách Hàng 2", 
                    Username = "khachhang2", 
                    Email = "kh2@gmail.com", 
                    Password = HashPassword("123456"),  // ✅ Hash password
                    Role = "Customer", 
                    CreatedAt = new DateTime(2025, 11, 24, 14, 10, 19) 
                }
            );

            // Seed Customers with hashed passwords
            modelBuilder.Entity<Customer>().HasData(
                new Customer 
                { 
                    CustomerId = 1, 
                    CustomerName = "Nguyễn Văn Admin", 
                    Username = "admin", 
                    Email = "admin@shop.com", 
                    Phone = "0901234567", 
                    Address = "123 Đường ABC, TP.HCM", 
                    City = "TP.HCM", 
                    PostalCode = "70000", 
                    PasswordHash = HashPassword("123456"),  // ✅ Hash password
                    Status = "Active", 
                    CreatedDate = DateTime.Now 
                },
                new Customer 
                { 
                    CustomerId = 2, 
                    CustomerName = "Trần Thị Nhân Viên", 
                    Username = "nhanvien", 
                    Email = "staff@shop.com", 
                    Phone = "0902345678", 
                    Address = "456 Đường DEF, TP.HCM", 
                    City = "TP.HCM", 
                    PostalCode = "70000", 
                    PasswordHash = HashPassword("123456"),  // ✅ Hash password
                    Status = "Active", 
                    CreatedDate = DateTime.Now 
                },
                new Customer 
                { 
                    CustomerId = 3, 
                    CustomerName = "Phạm Minh Khách", 
                    Username = "khachhang1", 
                    Email = "kh1@gmail.com", 
                    Phone = "0903456789", 
                    Address = "789 Đường GHI, TP.HCM", 
                    City = "TP.HCM", 
                    PostalCode = "70000", 
                    PasswordHash = HashPassword("MatKhauMoi_123"),  // ✅ Hash password
                    Status = "Active", 
                    CreatedDate = DateTime.Now 
                },
                new Customer 
                { 
                    CustomerId = 4, 
                    CustomerName = "Hoàng Ngọc Khách 2", 
                    Username = "khachhang2", 
                    Email = "kh2@gmail.com", 
                    Phone = "0904567890", 
                    Address = "101 Đường JKL, TP.HCM", 
                    City = "TP.HCM", 
                    PostalCode = "70000", 
                    PasswordHash = HashPassword("123456"),  // ✅ Hash password
                    Status = "Active", 
                    CreatedDate = DateTime.Now 
                }
            );
        }
    }
}
