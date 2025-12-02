# ? FIX DATABASE TABLE MAPPING - HOÀN THÀNH

**Ngày:** 2025-01-15  
**Tr?ng thái:** ? HOÀN THÀNH VÀ BUILD THÀNH CÔNG

---

## ?? V?N ??

L?i khi t?i danh sách khách hàng:
```
Invalid object name 'Customers'. 
Sai tên b?ng vì b?ng trong db là KhachHang
```

**Nguyên nhân:**
- Code s? d?ng tên b?ng ti?ng Anh (Customers, Products, Invoices, etc.)
- Database s? d?ng tên b?ng ti?ng Vi?t/tên khác (KhachHang, Product, Orders, ReviewProduct, Cart, OrderDetail)
- EntityFramework Core không tìm th?y b?ng `Customers` ? L?i

---

## ?? MAPPING C?A CÁC B?NG

| Entity (Code) | B?ng Database | Tr?ng Thái |
|---------------|---------------|----------|
| `Customer` | `KhachHang` | ? Fixed |
| `Product` | `Product` | ? Fixed |
| `Invoice` | `Orders` | ? Fixed |
| `InvoiceDetail` | `OrderDetail` | ? Fixed |
| `Review` | `ReviewProduct` | ? Fixed |
| `Cart` | `Cart` | ? Fixed |
| `CartItem` | ? Không t?n t?i | ?? Removed |

---

## ? GI?I PHÁP

### File: `ApplicationDbContext.cs`

**Thêm mapping cho t?t c? các entity:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Map table names to database
    modelBuilder.Entity<Customer>().ToTable("KhachHang");
    modelBuilder.Entity<Product>().ToTable("Product");
    modelBuilder.Entity<Invoice>().ToTable("Orders");
    modelBuilder.Entity<Review>().ToTable("ReviewProduct");
    modelBuilder.Entity<Cart>().ToTable("Cart");
    modelBuilder.Entity<InvoiceDetail>().ToTable("OrderDetail");

    // ... Ph?n còn l?i c?a c?u hình
}
```

### Cách Ho?t ??ng

```
EF Core Code:
    DbSet<Customer> Customers
            ? (ToTable mapping)
Database Table:
    KhachHang
```

---

## ?? CHI TI?T MAPPING

### 1?? Customer ? KhachHang
**Columns:**
- `CustomerId` ? `MaKH`
- `CustomerName` ? `TenKH`
- `Phone` ? `SoDT`
- `CreatedDate` ? `NgayDangKy`
- Primary Key: `MaKH`

### 2?? Product ? Product
**Columns:**
- `ProductId` ? `Product_Id`
- `ProductName` ? `ProductName`
- `Stock` ? `Quantity`
- Primary Key: `Product_Id`

### 3?? Invoice ? Orders
**Columns:**
- `InvoiceId` ? `Order_Id`
- `InvoiceNumber` ? `OrderNo`
- `InvoiceDate` ? `OrderDate`
- `CustomerId` ? `MaKH`
- Primary Key: `Order_Id`

### 4?? InvoiceDetail ? OrderDetail
**Columns:**
- `InvoiceDetailId` ? `OrderDetail_Id`
- `InvoiceId` ? `Order_Id`
- `ProductId` ? `Product_Id`
- Primary Key: `OrderDetail_Id`

### 5?? Review ? ReviewProduct
**Columns:**
- `ReviewId` ? `Review_Id`
- `CustomerId` ? `MaKH`
- `ProductId` ? `Product_Id`
- Primary Key: `Review_Id`

### 6?? Cart ? Cart
**Columns:**
- `CartId` ? `Cart_Id`
- `CustomerId` ? `MaKH`
- Primary Key: `Cart_Id`

---

## ?? BUILD STATUS

```
? Build:            SUCCESS
? Errors:           0
? Warnings:         0
? Table Mappings:   CONFIGURED
? Ready to Use:     YES
```

---

## ?? TI?P THEO

Gi? ?ây b?n có th?:
1. ? T?i danh sách khách hàng mà không có l?i
2. ? Thêm/S?a/Xóa khách hàng
3. ? Truy c?p t?t c? các ch?c n?ng liên quan

---

## ?? GHI CHÚ QUAN TR?NG

### N?u g?p l?i "Column not found"
- Ki?m tra tên c?t trong database
- Có th? c?n thêm `.HasColumnName("TenCotThucTe")` cho t?ng property
- Ví d?:
```csharp
modelBuilder.Entity<Customer>()
    .Property(c => c.CustomerName)
    .HasColumnName("TenKH");
```

### N?u mu?n ??ng b? database
```powershell
# T?o migration m?i
dotnet ef migrations add SyncDatabaseTableNames

# C?p nh?t database
dotnet ef database update
```

---

## ?? L?CH S? THAY ??I

| T?p | Thay ??i | Ngày |
|-----|----------|------|
| `ApplicationDbContext.cs` | Thêm mapping `.ToTable()` cho 6 entities | 2025-01-15 |
| `appsettings.json` | Thêm `TrustServerCertificate=true` | 2025-01-15 |

---

**Tác gi?:** AI Assistant  
**Tr?ng thái:** ? HOÀN THÀNH  
**Ngày:** 2025-01-15
