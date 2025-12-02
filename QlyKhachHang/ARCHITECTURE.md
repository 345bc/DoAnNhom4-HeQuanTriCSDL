# ??? KI?N TRÚC & THÀNH PH?N K? THU?T

## ?? M?c L?c
1. [T?ng Quan Ki?n Trúc](#t?ng-quan)
2. [C?u Trúc Th? M?c](#c?u-trúc-th?-m?c)
3. [Models (Mô Hình D? Li?u)](#models)
4. [Controllers (B? ?i?u Khi?n)](#controllers)
5. [Views (Giao Di?n)](#views)
6. [Database (C? S? D? Li?u)](#database)
7. [Services & Dependencies](#services)

---

## ??? T?ng Quan Ki?n Trúc

```
???????????????????????????????????????????????????????
?         Client Layer (Razor Views + HTML/CSS)       ?
?                                                     ?
?  ?? Customer Pages                                 ?
?  ?? Product Pages                                  ?
?  ?? Cart Pages                                     ?
?  ?? Invoice Pages                                  ?
?  ?? Payment Pages                                  ?
?  ?? Shared Layout                                  ?
???????????????????????????????????????????????????????
                 ?
???????????????????????????????????????????????????????
?      Application Layer (ASP.NET Core Controllers)   ?
?                                                     ?
?  ?? CustomerController                            ?
?  ?? ProductController                             ?
?  ?? CartController                                ?
?  ?? InvoiceController                             ?
?  ?? PaymentController                             ?
?  ?? AccountController                             ?
?  ?? HomeController                                ?
???????????????????????????????????????????????????????
                 ?
???????????????????????????????????????????????????????
?    Business Logic Layer (Services & Repositories)  ?
?                                                     ?
?  ?? AuthenticationService                         ?
?  ?? PaymentService                                ?
?  ?? InvoiceService                                ?
?  ?? ValidationService                             ?
???????????????????????????????????????????????????????
                 ?
???????????????????????????????????????????????????????
?     Data Access Layer (Entity Framework Core)      ?
?                                                     ?
?  ?? ApplicationDbContext                          ?
?  ?? DbSet<Customer>                               ?
?  ?? DbSet<Product>                                ?
?  ?? DbSet<Cart>                                   ?
?  ?? DbSet<Invoice>                                ?
?  ?? DbSet<Payment>                                ?
?  ?? Migration Files                               ?
???????????????????????????????????????????????????????
                 ?
???????????????????????????????????????????????????????
?         Database Layer (SQL Server)                 ?
?                                                     ?
?  ?? Customers Table                               ?
?  ?? Products Table                                ?
?  ?? Carts Table                                   ?
?  ?? CartItems Table                               ?
?  ?? Invoices Table                                ?
?  ?? InvoiceDetails Table                          ?
?  ?? Payments Table                                ?
?  ?? Reviews Table                                 ?
?  ?? Users Table                                   ?
???????????????????????????????????????????????????????
```

---

## ?? C?u Trúc Th? M?c

```
QlyKhachHang/
??? Controllers/
?   ??? HomeController.cs
?   ??? AccountController.cs
?   ??? CustomerController.cs
?   ??? ProductController.cs
?   ??? CartController.cs
?   ??? CartItemController.cs
?   ??? InvoiceController.cs
?   ??? InvoiceDetailController.cs
?   ??? PaymentController.cs
?   ??? ReviewController.cs
?
??? Models/
?   ??? Customer.cs
?   ??? Product.cs
?   ??? Cart.cs
?   ??? CartItem.cs
?   ??? Invoice.cs
?   ??? InvoiceDetail.cs
?   ??? Payment.cs
?   ??? Review.cs
?   ??? User.cs
?   ??? LoginViewModel.cs
?   ??? RegisterViewModel.cs
?   ??? ErrorViewModel.cs
?
??? Views/
?   ??? Home/
?   ?   ??? Index.cshtml
?   ?   ??? Privacy.cshtml
?   ??? Customer/
?   ?   ??? Index.cshtml
?   ?   ??? Create.cshtml
?   ?   ??? Edit.cshtml
?   ?   ??? Details.cshtml
?   ?   ??? Delete.cshtml
?   ??? Product/
?   ?   ??? Index.cshtml
?   ?   ??? Create.cshtml
?   ?   ??? Edit.cshtml
?   ?   ??? Details.cshtml
?   ?   ??? Delete.cshtml
?   ??? Cart/
?   ?   ??? Index.cshtml
?   ?   ??? Create.cshtml
?   ??? Invoice/
?   ?   ??? Index.cshtml
?   ?   ??? Create.cshtml
?   ?   ??? Edit.cshtml
?   ?   ??? Details.cshtml
?   ?   ??? Delete.cshtml
?   ??? Payment/
?   ?   ??? Index.cshtml
?   ?   ??? Create.cshtml
?   ?   ??? Edit.cshtml
?   ?   ??? Details.cshtml
?   ?   ??? Delete.cshtml
?   ??? Shared/
?   ?   ??? _Layout.cshtml
?   ?   ??? Error.cshtml
?   ?   ??? _ValidationScriptsPartial.cshtml
?   ??? _ViewStart.cshtml
?   ??? _ViewImports.cshtml
?
??? Services/
?   ??? AuthenticationService.cs
?
??? Data/
?   ??? ApplicationDbContext.cs
?
??? Migrations/
?   ??? AddAuthenticationFields.cs
?   ??? AddUsernameToUser.cs
?   ??? AddSeedData.cs
?   ??? AddCompleteBusinessData.cs
?   ??? AddPaymentModule.cs
?   ??? ApplicationDbContextModelSnapshot.cs
?
??? wwwroot/
?   ??? css/
?   ?   ??? site.css
?   ?   ??? fashion-shop.css
?   ?   ??? customer-management.css
?   ?   ??? payment-management.css
?   ??? js/
?   ?   ??? site.js
?   ?   ??? bootstrap.bundle.min.js
?   ??? lib/
?       ??? bootstrap/
?       ??? jquery/
?       ??? font-awesome/
?
??? Properties/
?   ??? launchSettings.json
?
??? Program.cs
??? appsettings.json
??? appsettings.Development.json
??? QlyKhachHang.csproj
??? README.md
```

---

## ?? Models (Mô Hình D? Li?u)

### 1. Customer (Khách Hàng)
```csharp
public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }        // Tên khách hàng
    public string Phone { get; set; }               // S? ?i?n tho?i
    public string Email { get; set; }               // Email
    public string Address { get; set; }             // ??a ch?
    public string City { get; set; }                // Thành ph?
    public DateTime? DateOfBirth { get; set; }      // Ngày sinh
    public string Gender { get; set; }              // Gi?i tính (Nam/N?)
    public string Username { get; set; }            // Tên ??ng nh?p
    public string Status { get; set; }              // Tr?ng thái
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    public DateTime? ModifiedDate { get; set; }     // Ngày ch?nh s?a
    
    // Navigation
    public ICollection<Cart> Carts { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}
```

### 2. Product (S?n Ph?m)
```csharp
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }         // Tên s?n ph?m
    public string Description { get; set; }         // Mô t?
    public decimal Price { get; set; }              // Giá
    public int Stock { get; set; }                  // T?n kho
    public string Category { get; set; }            // Danh m?c
    public string ImageUrl { get; set; }            // ???ng d?n hình ?nh
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    public DateTime? ModifiedDate { get; set; }     // Ngày ch?nh s?a
    
    // Navigation
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
}
```

### 3. Cart (Gi? Hàng)
```csharp
public class Cart
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }             // Mã khách hàng
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    public DateTime? ModifiedDate { get; set; }     // Ngày ch?nh s?a
    
    // Navigation
    public Customer Customer { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
```

### 4. CartItem (Chi Ti?t Gi? Hàng)
```csharp
public class CartItem
{
    public int CartItemId { get; set; }
    public int CartId { get; set; }                 // Mã gi? hàng
    public int ProductId { get; set; }              // Mã s?n ph?m
    public int Quantity { get; set; }               // S? l??ng
    public decimal UnitPrice { get; set; }          // Giá ??n v?
    
    // Navigation
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
```

### 5. Invoice (Hóa ??n)
```csharp
public class Invoice
{
    public int InvoiceId { get; set; }
    public string InvoiceNumber { get; set; }       // S? hóa ??n
    public int CustomerId { get; set; }             // Mã khách hàng
    public DateTime InvoiceDate { get; set; }       // Ngày l?p
    public decimal TotalAmount { get; set; }        // T?ng ti?n
    public decimal PaidAmount { get; set; }         // ?ã thanh toán
    public string Status { get; set; }              // Tr?ng thái
    public string Notes { get; set; }               // Ghi chú
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    
    // Navigation
    public Customer Customer { get; set; }
    public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    public ICollection<Payment> Payments { get; set; }
}
```

### 6. InvoiceDetail (Chi Ti?t Hóa ??n)
```csharp
public class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }
    public int InvoiceId { get; set; }              // Mã hóa ??n
    public int ProductId { get; set; }              // Mã s?n ph?m
    public int Quantity { get; set; }               // S? l??ng
    public decimal UnitPrice { get; set; }          // Giá ??n v?
    public decimal Subtotal { get; set; }           // T?ng c?ng
    
    // Navigation
    public Invoice Invoice { get; set; }
    public Product Product { get; set; }
}
```

### 7. Payment (Thanh Toán)
```csharp
public class Payment
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }              // Mã hóa ??n
    public decimal Amount { get; set; }             // S? ti?n
    public string PaymentMethod { get; set; }       // Ph??ng th?c
    public string Status { get; set; }              // Tr?ng thái
    public string TransactionId { get; set; }       // Mã giao d?ch
    public string BankName { get; set; }            // Tên ngân hàng
    public string BankAccountNumber { get; set; }   // S? tài kho?n
    public string BankAccountName { get; set; }     // Tên ch? TK
    public string Notes { get; set; }               // Ghi chú
    public DateTime PaymentDate { get; set; }       // Ngày thanh toán
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    
    // Navigation
    public Invoice Invoice { get; set; }
}
```

### 8. Review (?ánh Giá)
```csharp
public class Review
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }              // Mã s?n ph?m
    public int CustomerId { get; set; }             // Mã khách hàng
    public int Rating { get; set; }                 // ?i?m ?ánh giá (1-5)
    public string Comment { get; set; }             // Bình lu?n
    public DateTime CreatedDate { get; set; }       // Ngày t?o
    
    // Navigation
    public Product Product { get; set; }
    public Customer Customer { get; set; }
}
```

### 9. User (Ng??i Dùng)
```csharp
public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }            // Tên ??ng nh?p
    public string Email { get; set; }               // Email
    public string PasswordHash { get; set; }        // Hash m?t kh?u
    public string Role { get; set; }                // Vai trò (Admin/Employee/Customer)
    public bool IsActive { get; set; }              // Kích ho?t
    public DateTime CreatedDate { get; set; }       // Ngày t?o
}
```

---

## ?? Controllers (B? ?i?u Khi?n)

### CustomerController.cs
**Ch?c N?ng:**
- GET `/Customer` - Danh sách khách hàng
- GET `/Customer/Create` - Form t?o m?i
- POST `/Customer/Create` - L?u khách hàng m?i
- GET `/Customer/Edit/{id}` - Form ch?nh s?a
- POST `/Customer/Edit/{id}` - L?u thay ??i
- GET `/Customer/Details/{id}` - Xem chi ti?t
- GET `/Customer/Delete/{id}` - Form xóa
- POST `/Customer/Delete/{id}` - Th?c hi?n xóa

**Tìm Ki?m & L?c:**
```csharp
public IActionResult Index(
    string searchTerm = "",
    string sortBy = "CustomerName",
    string sortOrder = "asc",
    string statusFilter = "",
    int page = 1)
{
    // Tìm ki?m theo: Tên, S?T, Email, ??a ch?
    // L?c theo: Tr?ng thái
    // S?p x?p theo: Tên, Ngày t?o, Email
}
```

### ProductController.cs
**Ch?c N?ng:**
- GET `/Product` - Danh sách s?n ph?m
- GET `/Product/Create` - Form t?o m?i
- POST `/Product/Create` - L?u s?n ph?m m?i
- GET `/Product/Edit/{id}` - Form ch?nh s?a
- POST `/Product/Edit/{id}` - L?u thay ??i
- GET `/Product/Details/{id}` - Xem chi ti?t
- GET `/Product/Delete/{id}` - Form xóa
- POST `/Product/Delete/{id}` - Th?c hi?n xóa

### PaymentController.cs
**Ch?c N?ng:**
- GET `/Payment` - Danh sách thanh toán
- GET `/Payment/Create` - Form t?o m?i
- POST `/Payment/Create` - L?u thanh toán m?i
- GET `/Payment/Edit/{id}` - Form ch?nh s?a
- POST `/Payment/Edit/{id}` - L?u thay ??i
- GET `/Payment/Details/{id}` - Xem chi ti?t
- GET `/Payment/Delete/{id}` - Form xóa
- POST `/Payment/Delete/{id}` - Th?c hi?n xóa

**Logic T? ??ng:**
- Tính toán PaidAmount khi thêm thanh toán
- C?p nh?t tr?ng thái Invoice khi thanh toán ??
- Validate s? ti?n không ???c v??t quá t?ng hóa ??n

### InvoiceController.cs
**Ch?c N?ng:**
- GET `/Invoice` - Danh sách hóa ??n
- GET `/Invoice/Create` - Form t?o m?i
- POST `/Invoice/Create` - L?u hóa ??n m?i
- GET `/Invoice/Details/{id}` - Xem chi ti?t
- GET `/Invoice/UpdateStatus/{id}` - C?p nh?t tr?ng thái
- POST `/Invoice/UpdateStatus/{id}` - L?u tr?ng thái m?i

---

## ?? Views (Giao Di?n)

### Layout (_Layout.cshtml)
**Thành Ph?n:**
- Header (Logo, Menu)
- Sidebar (Danh sách module)
- Main Content
- Footer

**Navigation Menu:**
```
Danh Sách Khách Hàng
Danh Sách S?n Ph?m
Danh Sách Gi? Hàng
Danh Sách Hóa ??n
Danh Sách Thanh Toán
Danh Sách ?ánh Giá
```

### View Pages
- **Create.cshtml** - Form t?o m?i
- **Edit.cshtml** - Form ch?nh s?a
- **Details.cshtml** - Xem chi ti?t
- **Delete.cshtml** - Xác nh?n xóa
- **Index.cshtml** - Danh sách d? li?u

---

## ?? Database (C? S? D? Li?u)

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QlyKhachHang;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```

### Tables
```sql
-- Khách hàng
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    City NVARCHAR(50),
    DateOfBirth DATETIME,
    Gender NVARCHAR(10),
    Username NVARCHAR(50),
    Status NVARCHAR(20),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME
);

-- S?n ph?m
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(12,2) NOT NULL,
    Stock INT NOT NULL,
    Category NVARCHAR(50),
    ImageUrl NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME
);

-- Hóa ??n
CREATE TABLE Invoices (
    InvoiceId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId INT NOT NULL,
    InvoiceDate DATETIME NOT NULL,
    TotalAmount DECIMAL(12,2) NOT NULL,
    PaidAmount DECIMAL(12,2) DEFAULT 0,
    Status NVARCHAR(50),
    Notes NVARCHAR(MAX),
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- Chi ti?t hóa ??n
CREATE TABLE InvoiceDetails (
    InvoiceDetailId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(12,2) NOT NULL,
    Subtotal DECIMAL(12,2) NOT NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

-- Thanh toán
CREATE TABLE Payments (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceId INT NOT NULL,
    Amount DECIMAL(12,2) NOT NULL,
    PaymentMethod NVARCHAR(50),
    Status NVARCHAR(50),
    TransactionId NVARCHAR(100),
    BankName NVARCHAR(100),
    BankAccountNumber NVARCHAR(50),
    BankAccountName NVARCHAR(100),
    Notes NVARCHAR(MAX),
    PaymentDate DATETIME NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId)
);

-- Gi? hàng
CREATE TABLE Carts (
    CartId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- Chi ti?t gi? hàng
CREATE TABLE CartItems (
    CartItemId INT PRIMARY KEY IDENTITY(1,1),
    CartId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(12,2) NOT NULL,
    FOREIGN KEY (CartId) REFERENCES Carts(CartId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
```

---

## ?? Services & Dependencies

### AuthenticationService.cs
```csharp
public class AuthenticationService
{
    private readonly ApplicationDbContext _context;
    
    public AuthenticationService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> LoginAsync(string usernameOrEmail, string password)
    {
        // Tìm User theo Username ho?c Email
        // So sánh m?t kh?u
        // Tr? v? User n?u ?úng, null n?u sai
    }
}
```

### Dependency Injection (Program.cs)
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthenticationService>();
```

---

## ?? Entity Framework Core

### DbContext
```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // C?u hình quan h?
        // Thêm d? li?u m?c ??nh
    }
}
```

---

**C?p nh?t:** 2024
**Phiên b?n:** 1.0
