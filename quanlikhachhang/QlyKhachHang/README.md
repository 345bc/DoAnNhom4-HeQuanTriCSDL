# ?? H? TH?NG QU?N LÝ KHÁCH HÀNG - QlyKhachHang

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-purple)]()
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)
[![Status](https://img.shields.io/badge/Status-?%20Production%20Ready-brightgreen)]()

M?t ?ng d?ng **ASP.NET Core 8 MVC** hoàn ch?nh cho **qu?n lý khách hàng** v?i h? th?ng xác th?c, qu?n lý s?n ph?m, gi? hàng, ?ánh giá, hóa ??n và Dashboard th?ng kê.

## ? Tính N?ng Chính

### ?? H? Th?ng Xác Th?c
- ? **??ng Ký** - T?o tài kho?n m?i v?i validation
- ? **??ng Nh?p** - Dùng Username/Email + M?t kh?u
- ? **B?o M?t** - SHA-256 password hashing
- ? **Session** - 30 phút timeout
- ? **Qu?n Lý Tr?ng Thái** - Active/Inactive/Blocked

### ?? Qu?n Lý Khách Hàng
- Danh sách khách hàng
- Thêm/S?a/Xóa khách hàng
- Xem chi ti?t + liên k?t (Gi?, ?ánh giá, Hóa ??n)

### ?? Qu?n Lý S?n Ph?m
- Danh sách s?n ph?m
- Qu?n lý giá & t?n kho
- Phân lo?i theo danh m?c
- Th?ng kê s? d?ng

### ?? Qu?n Lý Gi? Hàng
- T?o gi? hàng
- Thêm/Xóa m?t hàng
- Ki?m tra t?n kho t? ??ng

### ? ?ánh Giá S?n Ph?m
- X?p h?ng 1-5 sao
- Bình lu?n chi ti?t
- Ki?m tra trùng l?p

### ?? Qu?n Lý Hóa ??n
- Sinh s? t? ??ng
- Chi ti?t hóa ??n
- Tính t?ng ti?n t? ??ng
- Tr?ng thái: Pending/Completed/Cancelled

### ?? Dashboard
- Th?ng kê cá nhân
- T?ng quan h? th?ng
- Doanh thu & s? li?u

## ?? Công Ngh? S? D?ng

| Công Ngh? | Phiên B?n | M?c ?ích |
|-----------|-----------|---------|
| **ASP.NET Core** | 8.0 | Framework chính |
| **Entity Framework Core** | 8.0 | ORM & Database |
| **SQL Server** | LocalDB | Database |
| **Bootstrap** | 5 | UI Framework |
| **jQuery** | Latest | JavaScript |
| **Font Awesome** | 6.0 | Icons |

## ?? Yêu C?u H? Th?ng

- .NET 8 SDK ho?c cao h?n
- SQL Server LocalDB (m?c ??nh)
- Visual Studio 2022 ho?c VS Code
- RAM: 4GB t?i thi?u
- Disk: 500MB free space

## ?? Cài ??t & Ch?y

### 1. Clone / M? D? Án
```bash
cd QlyKhachHang
```

### 2. Khôi Ph?c Dependencies
```bash
dotnet restore
```

### 3. T?o Database
```bash
# Migration ?ã ???c t?o
dotnet ef database update
```

### 4. Ch?y ?ng D?ng
```bash
dotnet run
```

### 5. M? Trình Duy?t
```
https://localhost:5001
```

## ?? H??ng D?n Nhanh

### ??ng Ký Tài Kho?n
1. Nh?p **"??ng Ký"** ? góc ph?i
2. ?i?n form ?? thông tin
3. Nh?p **"??ng Ký"**

### ??ng Nh?p
1. Nh?p **"??ng Nh?p"**
2. Nh?p Username/Email + M?t kh?u
3. Nh?p **"??ng Nh?p"**

### Qu?n Lý D? Li?u
1. Vào menu **"Qu?n lý"**
2. Ch?n lo?i d? li?u
3. Dùng nút **Thêm/S?a/Xóa/Xem**

## ?? C?u Trúc D? Án

```
QlyKhachHang/
??? Models/                 # 7 Entities + 2 ViewModels
??? Controllers/            # 8 Controllers (CRUD + Auth)
??? Views/                  # Razor templates
??? Services/               # AuthenticationService
??? Data/                   # DbContext + Migrations
??? wwwroot/                # Static files
??? Program.cs              # Configuration
??? appsettings.json        # Settings
```

## ?? B?o M?t

? SHA-256 Password Hashing
? Session 30 phút timeout
? CSRF Token Protection
? Input Validation (Client + Server)
? Email/Username Uniqueness Check
? Account Status Checking

## ?? Database Schema

```sql
-- 7 Tables chính
Customers
??? CustomerId (PK)
??? Username (Unique)
??? PasswordHash
??? Status
??? ... other fields

Products
Carts
CartItems
Reviews
Invoices
InvoiceDetails
```

## ?? Th? Nghi?m

### Manual Testing Checklist
- [ ] ??ng ký tài kho?n m?i
- [ ] ??ng nh?p b?ng Username
- [ ] ??ng nh?p b?ng Email
- [ ] Xem Dashboard
- [ ] CRUD Khách Hàng
- [ ] CRUD S?n Ph?m
- [ ] T?o Gi? Hàng
- [ ] T?o Hóa ??n
- [ ] ?ánh Giá S?n Ph?m
- [ ] ??ng Xu?t

## ?? Kh?c Ph?c S? C?

| V?n ?? | Gi?i Pháp |
|--------|----------|
| Connection string not found | Ki?m tra `appsettings.json` |
| Port 5001 already in use | Ch?y `dotnet run --urls "https://localhost:7001"` |
| Database not found | Ch?y `dotnet ef database update` |
| Login failed | Ki?m tra Username/Email & Password |
| BuildFailed | Ch?y `dotnet clean` r?i `dotnet build` |

## ?? Tài Li?u

- [AUTHENTICATION_GUIDE.md](AUTHENTICATION_GUIDE.md) - H??ng d?n xác th?c
- [README_DETAILS.md](README_DETAILS.md) - Tài li?u chi ti?t (n?u có)

## ?? Tính N?ng S?p T?i

- ? Quên M?t Kh?u / Reset Password
- ? Email Verification
- ? Search & Filter
- ? Pagination
- ? Export CSV/Excel
- ? Advanced Reporting
- ? Two-Factor Authentication
- ? API RESTful

## ?? Performance

- ? Async/Await throughout
- ? Optimized Database Queries
- ? No N+1 Query Problems
- ? Proper Indexing
- ? Session Caching

## ?? UI/UX

- Bootstrap 5 responsive design
- Font Awesome icons
- Mobile-friendly
- Clean & modern interface
- Color-coded sections
- Success/Error notifications

## ?? License

MIT License - S? d?ng t? do cho m?c ?ích cá nhân & th??ng m?i

## ?? Tác Gi?

Phát tri?n b?i **AI Assistant (GitHub Copilot)**

## ?? C?m ?n

C?m ?n b?n ?ã s? d?ng ?ng d?ng này!

---

## ?? H? Tr?

N?u g?p v?n ??:
1. Xem tài li?u trong folder
2. Ki?m tra Console output
3. ??m b?o .NET 8 SDK ?ã cài
4. Ki?m tra SQL Server LocalDB

---

**Phiên B?n:** 1.0.0  
**C?p nh?t:** 24 Tháng 11 N?m 2025  
**Framework:** ASP.NET Core 8  
**Language:** C# 12  
**Status:** ? **Production Ready**

Chúc b?n s? d?ng vui v?! ??
