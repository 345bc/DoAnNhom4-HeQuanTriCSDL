# ?? H? TH?NG XÁC TH?C - H??NG D?N NHANH

## ? HOÀN THÀNH

H? th?ng xác th?c ?ã ???c **tri?n khai hoàn ch?nh** v?i:

- ? **??ng Ký (Register)** - T?o tài kho?n m?i
- ? **??ng Nh?p (Login)** - ??ng nh?p b?ng Username/Email
- ? **??ng Xu?t (Logout)** - Xóa Session
- ? **Mã Hóa M?t Kh?u** - SHA-256
- ? **Session Management** - 30 phút timeout
- ? **Navigation Menu** - Hi?n th? tên ng??i dùng
- ? **Dashboard** - Th?ng kê cho ng??i ?ã ??ng nh?p
- ? **Database Migration** - C?p nh?t thành công

## ?? T?P TIN ???C T?O/C?P NH?T

### Models
- ? `Models/LoginViewModel.cs` - Form ??ng nh?p
- ? `Models/RegisterViewModel.cs` - Form ??ng ký
- ? `Models/Customer.cs` - Thêm Auth fields (Username, PasswordHash, Status, LastLoginDate)

### Services
- ? `Services/AuthenticationService.cs` - Logic xác th?c & b?o m?t

### Controllers
- ? `Controllers/AccountController.cs` - Register, Login, Logout
- ? `Controllers/HomeController.cs` - Dashboard

### Views
- ? `Views/Account/Login.cshtml` - Trang ??ng nh?p
- ? `Views/Account/Register.cshtml` - Trang ??ng ký
- ? `Views/Home/Index.cshtml` - Dashboard
- ? `Views/Shared/_Layout.cshtml` - Menu Login/Logout

### Configuration
- ? `Program.cs` - AuthenticationService + Session
- ? Database Migration `AddAuthenticationFields`

## ?? CH?Y ?NG D?NG

```bash
# 1. M? th? m?c d? án
cd QlyKhachHang

# 2. Ch?y ?ng d?ng
dotnet run

# 3. M? trình duy?t
https://localhost:5001
```

## ?? H??NG D?N S? D?NG

### 1?? **??NG KÝ TÀI KHO?N**
1. Truy c?p: `https://localhost:5001`
2. Nh?p "??ng Ký" ? góc ph?i
3. ?i?n thông tin:
   - Tên khách hàng
   - Tên ??ng nh?p (3-50 ký t?)
   - Email
   - S? ?i?n tho?i
   - ??a ch?, Thành ph?
   - M?t kh?u (t?i thi?u 6 ký t?)
   - Xác nh?n m?t kh?u
   - ??ng ý ?i?u kho?n
4. Nh?p "??ng Ký"
5. Chuy?n sang trang ??ng Nh?p

### 2?? **??NG NH?P**
1. Nh?p "??ng Nh?p"
2. Nh?p **Username ho?c Email**
3. Nh?p **M?t kh?u**
4. Ch?n "Nh? tôi" (tùy ch?n)
5. Nh?p "??ng Nh?p"
6. Xem Dashboard

### 3?? **DASHBOARD (Sau ??ng Nh?p)**
Hi?n th?:
- Thông tin cá nhân (Tên, Email, S?T)
- S? gi? hàng c?a b?n
- S? ?ánh giá c?a b?n
- S? hóa ??n c?a b?n
- T?ng chi tiêu
- Th?ng kê toàn h? th?ng

### 4?? **QU?N LÝ D? LI?U**
Sau khi ??ng nh?p, menu "Qu?n lý" s? hi?n th?:
- ?? Khách Hàng
- ?? S?n Ph?m
- ?? Gi? Hàng
- ?? Chi Ti?t Gi? Hàng
- ? ?ánh Giá
- ?? Hóa ??n
- ?? Chi Ti?t Hóa ??n

### 5?? **??NG XU?T**
1. Nh?p **Avatar** (??) ? góc ph?i
2. Ch?n **??ng Xu?t**
3. Session s? b? xóa
4. Quay v? trang ch?

## ?? CHI TI?T B?O M?T

| Tính N?ng | Chi Ti?t |
|-----------|----------|
| **Mã Hóa M?t Kh?u** | SHA-256 |
| **Session Timeout** | 30 phút không ho?t ??ng |
| **Ki?m Tra Email** | Không ???c trùng l?p |
| **Ki?m Tra Username** | Không ???c trùng l?p (3-50 ký t?) |
| **Ki?m Tra Tr?ng Thái** | Ch? tài kho?n "Active" m?i ??ng nh?p ???c |
| **CSRF Token** | Trên t?t c? form POST |
| **Validation** | C? client-side & server-side |
| **Last Login** | T? ??ng ghi nh?n th?i gian ??ng nh?p |

## ?? D? LI?U M?UÔI CÓ S?N

?ã có 3 khách hàng seed data (nh?ng không có Username/Password):
- Nguy?n V?n A
- Tr?n Th? B
- Ph?m V?n C

**?? ??ng nh?p, vui lòng ??NG KÝ tài kho?n m?i**

## ?? C?U HÌNH

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=QlyKhachHangDb;Trusted_Connection=true;MultipleActiveResultSets=true;"
  }
}
```

### Session (Program.cs)
```csharp
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
```

## ?? KH?C PH?C S? C?

| L?i | Gi?i Pháp |
|-----|----------|
| "Connection string not found" | Ki?m tra `appsettings.json` |
| "Port 5001 is already in use" | Ch?y v?i port khác ho?c ?óng ?ng d?ng khác |
| "Database could not be opened" | Ch?y l?i `dotnet ef database update` |
| "Login failed" | Ki?m tra Username/Email và M?t kh?u |
| "Email/Username already exists" | S? d?ng email/username khác |

## ?? KI?M TRA CH?C N?NG

```
? ??ng ký tài kho?n m?i
? ??ng nh?p b?ng Username
? ??ng nh?p b?ng Email
? Quên m?t kh?u (ch?a có)
? Xem Dashboard
? Truy c?p menu Qu?n lý
? ??ng xu?t
? Session timeout 30 phút
```

## ?? TÍNH N?NG S?P T?I (Optional)

- ? Quên M?t Kh?u
- ? Reset Password via Email
- ? Email Verification
- ? Two-Factor Authentication
- ? Role-based Authorization (Admin/User)
- ? Account Deactivation

## ?? LIÊN H?

N?u g?p v?n ??:
1. Xem Console output
2. Ki?m tra Application logs
3. Xem file Exception

---

**Status:** ? PRODUCTION READY
**Phiên B?n:** 1.0.0
**C?p nh?t:** 24 Tháng 11 N?m 2025
