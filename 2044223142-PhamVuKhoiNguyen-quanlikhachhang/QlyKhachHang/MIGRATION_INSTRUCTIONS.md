# 🔧 HƯỚNG DẪN CHẠY MIGRATION

## 📌 BẮT BUỘC PHẢI LÀM TRƯỚC KHI RUN

### Bước 1: Mở Terminal / PowerShell

Nhấn `Ctrl + Backtick` trong Visual Studio hoặc mở PowerShell

### Bước 2: Điều hướng tới thư mục project

```powershell
cd D:\qly_thoitrang\QlyKhachHang\QlyKhachHang
```

Hoặc:
```powershell
cd .\QlyKhachHang
```

### Bước 3: Chạy Migration

```powershell
dotnet ef database update
```

**Output dự kiến:**
```
Building...
Microsoft.EntityFrameworkCore.Database.Command: Executed DbCommand (89ms)
CREATE TABLE [Users] (...)
ALTER TABLE [Users] ADD COLUMN Username ...
CREATE UNIQUE INDEX [IX_Users_Username] ...
```

---

## ⚠️ NẾU CÓ LỖI

### Lỗi: "No migrations found"

```powershell
dotnet ef migrations list
```

Nếu chưa có migration, chạy:
```powershell
dotnet ef migrations add AddUsernameToUser
dotnet ef database update
```

### Lỗi: "Unable to connect to database"

Kiểm tra connection string trong `appsettings.json`:
```json
"DefaultConnection": "Server=YOUR_SERVER;Database=QlyKhachHang;Trusted_Connection=true;TrustServerCertificate=true"
```

---

## ✅ KIỂM CHỨNG SAU MIGRATION

### 1. Kiểm tra Database

Mở SQL Server Management Studio:
1. Connect tới database `QlyKhachHang`
2. Vào `Tables` → `dbo.Users`
3. Kiểm tra có column `Username` chưa

### 2. Kiểm tra dữ liệu seed

```sql
SELECT UserID, Name, Username, Email, Role 
FROM Users
ORDER BY UserID
```

**Kết quả dự kiến:**
```
UserID  Name            Username      Email                Role
1       Admin           admin         admin@shop.com       Admin
2       Nhân Viên       nhanvien      staff@shop.com       Employee
3       Khách Hàng 1    khachhang1    kh1@gmail.com        Customer
4       Khách Hàng 2    khachhang2    kh2@gmail.com        Customer
```

---

## 🚀 CHẠY ỨNG DỤNG

Sau khi migration thành công:

```powershell
dotnet run
```

Hoặc nhấn `F5` trong Visual Studio

---

## 📋 TROUBLESHOOTING

| Vấn đề | Giải pháp |
|--------|----------|
| `DbContext` không tìm thấy | Kiểm tra `Program.cs` có đăng ký DbContext |
| Migration failed | Xóa DB cũ, chạy `dotnet ef database update` lại |
| Connection timeout | Kiểm tra SQL Server đang chạy |
| Constraint violation | Username đã tồn tại - kiểm tra dữ liệu cũ |

---

## 🔍 LỆNH HỮPEFUL

```powershell
# Xem tất cả migrations
dotnet ef migrations list

# Xem lịch sử migrations
dotnet ef migrations list --verbose

# Rollback migration gần nhất
dotnet ef database update {PreviousMigrationName}

# Xóa migration (chỉ nếu chưa apply)
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

---

## ✅ KIỂM DANH CÓ READY CHƯA?

- [ ] Terminal/PowerShell opened
- [ ] Điều hướng tới folder `QlyKhachHang`
- [ ] `dotnet ef database update` chạy thành công
- [ ] SQL Server Management Studio kiểm tra Username column có
- [ ] `dotnet run` không có lỗi
- [ ] Truy cập `https://localhost:5001` được

**✅ TẤT CẢ ✓ → SẴN SÀNG TEST ĐĂNG NHẬP!**
