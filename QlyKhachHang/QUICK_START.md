# 🚀 QUICK START COMMANDS

Sao chép các lệnh dưới đây và chạy trong Terminal/PowerShell

---

## 📍 Step 1: Navigate to Project

```powershell
cd D:\qly_thoitrang\QlyKhachHang\QlyKhachHang
```

Hoặc nếu đã trong folder QlyKhachHang:
```powershell
cd .\QlyKhachHang
```

---

## 🔄 Step 2: Apply Database Migration

```powershell
dotnet ef database update
```

⏳ Chờ cho tới khi hoàn tất

---

## ▶️ Step 3: Run Application

```powershell
dotnet run
```

hoặc nhấn `F5` trong Visual Studio

---

## 🌐 Step 4: Open Browser

```
https://localhost:5001/Account/Login
```

---

## 🔐 Step 5: Test Login

### Cách 1: Username
```
Tên đăng nhập hoặc Email: admin
Mật khẩu: 123456
Click: ĐĂNG NHẬP
```

### Cách 2: Email
```
Tên đăng nhập hoặc Email: admin@shop.com
Mật khẩu: 123456
Click: ĐĂNG NHẬP
```

---

## 🧪 Other Test Accounts

```
nhanvien / 123456
khachhang1 / MatKhauMoi_123
khachhang2 / 123456
```

---

## 🛠️ Troubleshooting Commands

### Xem danh sách migrations
```powershell
dotnet ef migrations list
```

### Rollback migration
```powershell
dotnet ef database update {MigrationName}
```

### Xóa migration (nếu chưa apply)
```powershell
dotnet ef migrations remove
```

### Clean rebuild
```powershell
dotnet clean
dotnet build
```

---

## ✅ Verification Checklist

Chạy từng lệnh để kiểm chứng:

```powershell
# 1. Check .NET version
dotnet --version

# 2. Check EF Core tools
dotnet ef --version

# 3. List databases
sqlcmd -S . -Q "SELECT name FROM sys.databases WHERE name = 'QlyKhachHang'"

# 4. List tables
sqlcmd -S . -d QlyKhachHang -Q "SELECT name FROM sys.tables"
```

---

## 📝 Log Files Location

Application logs:
```
D:\qly_thoitrang\QlyKhachHang\QlyKhachHang\bin\Debug\net8.0\
```

---

## 🆘 Emergency Reset

Nếu muốn reset database hoàn toàn:

```powershell
# 1. Remove migration
dotnet ef migrations remove

# 2. Delete database
sqlcmd -S . -Q "DROP DATABASE QlyKhachHang"

# 3. Recreate migration
dotnet ef migrations add InitialCreate

# 4. Update database
dotnet ef database update
```

---

## 📞 Common Issues & Quick Fixes

### Build fails
```powershell
dotnet clean
dotnet build
```

### Database connection error
```powershell
# Check SQL Server is running
sqlcmd -S .
```

### Port already in use
```powershell
# Use different port
dotnet run --urls="https://localhost:7000"
```

### Hot reload issues
- Close app completely
- Run `dotnet run` again

---

## 🎯 Success Indicators

✅ You should see:
- Build successful message
- "Application started" in console
- Can access https://localhost:5001
- Login page displays correctly
- Can login with admin/123456

---

**Need more help? Check these files:**
- 📋 `LOGIN_GUIDE.md` - User guide
- 🔧 `MIGRATION_INSTRUCTIONS.md` - Database setup
- 📊 `LOGIN_SYSTEM_COMPLETED.md` - Technical details
- 🎨 `LOGIN_VISUAL_GUIDE.md` - Visual diagrams
