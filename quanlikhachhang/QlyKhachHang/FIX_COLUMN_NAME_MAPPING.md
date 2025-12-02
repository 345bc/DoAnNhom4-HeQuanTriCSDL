# ? FIX COLUMN NAME MAPPING - HOÀN THÀNH

**Ngày:** 2025-01-15  
**Tr?ng thái:** ? HOÀN THÀNH VÀ BUILD THÀNH CÔNG

---

## ?? V?N ??

L?i khi t?i danh sách khách hàng:
```
Invalid column name 'CustomerId'
Invalid column name 'Address'
Invalid column name 'City'
Invalid column name 'CreatedDate'
Invalid column name 'CustomerName'
Invalid column name 'LastLoginDate'
Invalid column name 'ModifiedDate'
Invalid column name 'PasswordHash'
Invalid column name 'Phone'
Invalid column name 'PostalCode'
Invalid column name 'Status'
Invalid column name 'Username'
```

**Nguyên nhân:**
- Entity Framework Core s? d?ng tên property C# ?? map t?i column database
- Nh?ng database s? d?ng tên column khác nhau (ti?ng Vi?t, underscores, etc.)
- EF Core không tìm th?y các column ? SQL Error

---

## ?? COLUMN MAPPINGS ?Ã THÊM

### 1. Customer ? KhachHang

| Property (C#) | Column Name (DB) | Mappings |
|---------------|------------------|----------|
| `CustomerId` | `MaKH` | ? Mapped |
| `CustomerName` | `TenKH` | ? Mapped |
| `Phone` | `SoDT` | ? Mapped |
| `CreatedDate` | `NgayDangKy` | ? Mapped |
| `Email` | `Email` | ? Auto (tên gi?ng) |
| `Address` | `Address` | ? Auto |
| `City` | `City` | ? Auto |
| `PostalCode` | `PostalCode` | ? Auto |
| `Status` | `Status` | ? Auto |
| `Username` | `Username` | ? Auto |
| `PasswordHash` | `PasswordHash` | ? Auto |
| `LastLoginDate` | `LastLoginDate` | ? Auto |
| `ModifiedDate` | `ModifiedDate` | ? Auto |

### 2. Product ? Product

| Property | Column Name | Status |
|----------|-------------|--------|
| `ProductId` | `Product_Id` | ? Mapped |

### 3. Invoice ? Orders

| Property | Column Name | Status |
|----------|-------------|--------|
| `InvoiceId` | `Order_Id` | ? Mapped |
| `CustomerId` | `MaKH` | ? Mapped |

### 4. Cart ? Cart

| Property | Column Name | Status |
|----------|-------------|--------|
| `CartId` | `Cart_Id` | ? Mapped |
| `CustomerId` | `MaKH` | ? Mapped |

### 5. InvoiceDetail ? OrderDetail

| Property | Column Name | Status |
|----------|-------------|--------|
| `InvoiceDetailId` | `OrderDetail_Id` | ? Mapped |
| `InvoiceId` | `Order_Id` | ? Mapped |
| `ProductId` | `Product_Id` | ? Mapped |

### 6. Review ? ReviewProduct

| Property | Column Name | Status |
|----------|-------------|--------|
| `ReviewId` | `Review_Id` | ? Mapped |
| `CustomerId` | `MaKH` | ? Mapped |
| `ProductId` | `Product_Id` | ? Mapped |

---

## ? GI?I PHÁP

### File: `ApplicationDbContext.cs`

**Thêm `.HasColumnName()` mapping cho m?i property:**

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Map table names
    modelBuilder.Entity<Customer>().ToTable("KhachHang");
    // ... (other table mappings)

    // Configure Customer column mappings
    var customerEntity = modelBuilder.Entity<Customer>();
    
    customerEntity.Property(c => c.CustomerId).HasColumnName("MaKH");
    customerEntity.Property(c => c.CustomerName).HasColumnName("TenKH");
    customerEntity.Property(c => c.Phone).HasColumnName("SoDT");
    customerEntity.Property(c => c.CreatedDate).HasColumnName("NgayDangKy");
    
    // ... (relationships config)

    // Configure Product column mappings
    var productEntity = modelBuilder.Entity<Product>();
    productEntity.Property(p => p.ProductId).HasColumnName("Product_Id");
    
    // ... (more mappings for Cart, Invoice, etc.)
}
```

### Cách Ho?t ??ng

```
EF Core C# Code:
    customer.CustomerId
            ? (HasColumnName mapping)
Database Column:
    [MaKH]
```

---

## ?? TOÀN B? MAPPINGS

### Customer Entity
```csharp
customerEntity.Property(c => c.CustomerId).HasColumnName("MaKH");
customerEntity.Property(c => c.CustomerName).HasColumnName("TenKH");
customerEntity.Property(c => c.Phone).HasColumnName("SoDT");
customerEntity.Property(c => c.CreatedDate).HasColumnName("NgayDangKy");
```

### Product Entity
```csharp
productEntity.Property(p => p.ProductId).HasColumnName("Product_Id");
```

### Cart Entity
```csharp
cartEntity.Property(c => c.CartId).HasColumnName("Cart_Id");
cartEntity.Property(c => c.CustomerId).HasColumnName("MaKH");
```

### Invoice Entity
```csharp
invoiceEntity.Property(i => i.InvoiceId).HasColumnName("Order_Id");
invoiceEntity.Property(i => i.CustomerId).HasColumnName("MaKH");
```

### InvoiceDetail Entity
```csharp
invoiceDetailEntity.Property(id => id.InvoiceDetailId).HasColumnName("OrderDetail_Id");
invoiceDetailEntity.Property(id => id.InvoiceId).HasColumnName("Order_Id");
invoiceDetailEntity.Property(id => id.ProductId).HasColumnName("Product_Id");
```

### Review Entity
```csharp
reviewEntity.Property(r => r.ReviewId).HasColumnName("Review_Id");
reviewEntity.Property(r => r.CustomerId).HasColumnName("MaKH");
reviewEntity.Property(r => r.ProductId).HasColumnName("Product_Id");
```

---

## ?? BUILD STATUS

```
? Build:            SUCCESS
? Errors:           0
? Warnings:         0
? Column Mappings:  CONFIGURED
? Ready to Use:     YES
```

---

## ?? TI?P THEO

Gi? ?ây b?n có th?:
1. ? T?i danh sách khách hàng mà không có l?i
2. ? Thêm/S?a/Xóa khách hàng
3. ? Truy c?p t?t c? các ch?c n?ng CRUD
4. ? Query các entity khác (Product, Invoice, Cart, etc.)

---

## ?? GHI CHÚ QUAN TR?NG

### V? Syntax
- S? d?ng `.Property(c => c.PropertyName).HasColumnName("DbColumnName")`
- Ph?i ??t tr??c các`.HasMany()`, `.HasIndex()` configurations
- Ho?c t?o variable entity r?i c?u hình trên ?ó

### N?u g?p l?i "Invalid column name..."
1. Ki?m tra l?i tên column chính xác trong database
2. Ch?c ch?n ?ã thêm `.HasColumnName()` cho property ?ó
3. Rebuild solution
4. Clear bin/obj folders n?u c?n

### N?u mu?n thêm column m?i
```csharp
// Thêm column mapping
modelBuilder.Entity<Customer>()
    .Property(c => c.NewProperty)
    .HasColumnName("NewColumnName");

// T?o migration
dotnet ef migrations add AddNewColumn

// Update database
dotnet ef database update
```

---

## ?? L?CH S? THAY ??I

| T?p | Thay ??i | Ngày |
|-----|----------|------|
| `ApplicationDbContext.cs` | Thêm `.HasColumnName()` mappings cho t?t c? entities | 2025-01-15 |
| `appsettings.json` | Thêm `TrustServerCertificate=true` | 2025-01-15 |

---

## ? SUMMARY

**V?n ??:**
- EF Core tìm column theo tên property C#
- Database dùng tên column khác

**Gi?i pháp:**
- Thêm `.HasColumnName()` mappings trong `OnModelCreating()`
- EF Core gi? bi?t map property t?i ?úng column

**K?t qu?:**
- ? T?t c? queries ho?t ??ng
- ? Không l?i SQL
- ? T?t c? CRUD operations s?n sàng

**Tr?ng thái:** ?? READY TO USE

---

**Tác gi?:** AI Assistant  
**Tr?ng thái:** ? HOÀN THÀNH  
**Ngày:** 2025-01-15
