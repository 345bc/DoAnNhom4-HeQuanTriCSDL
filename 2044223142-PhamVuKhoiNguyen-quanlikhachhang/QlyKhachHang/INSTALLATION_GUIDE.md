# ?? H??NG D?N CÀI ??T & TRI?N KHAI

## ?? Yêu C?u H? Th?ng

### Ph?n M?m B?t Bu?c
- **Windows 10/11** ho?c **Linux/macOS**
- **Visual Studio 2022** (ho?c VS Code + .NET SDK)
- **.NET 8 SDK** tr? lên
- **SQL Server 2019** tr? lên (ho?c SQL Server Express)
- **Git** (?? clone repository)

### Ph?n C?ng T?i Thi?u
- **CPU:** Intel i5 ho?c t??ng ???ng
- **RAM:** 4GB (t?i thi?u), 8GB (khuy?n ngh?)
- **? c?ng:** 2GB dung l??ng tr?ng
- **Internet:** ?? t?i các package NuGet

---

## ?? Cài ??t

### B??c 1: Cài ??t .NET SDK

#### Trên Windows
1. Truy c?p: https://dotnet.microsoft.com/download
2. T?i **.NET 8 SDK** (phiên b?n m?i nh?t)
3. Ch?y installer và làm theo h??ng d?n
4. Ki?m tra cài ??t:
   ```bash
   dotnet --version
   ```

#### Trên Linux
```bash
# Ubuntu/Debian
sudo apt-get update
sudo apt-get install dotnet-sdk-8.0

# Ki?m tra
dotnet --version
```

#### Trên macOS
```bash
# S? d?ng Homebrew
brew install dotnet

# Ki?m tra
dotnet --version
```

### B??c 2: Cài ??t SQL Server

#### Trên Windows
1. T?i **SQL Server 2019 Express** t?: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
2. Ch?y installer
3. Ch?n "Custom" installation
4. Cài ??t SQL Server Engine
5. C?u hình xác th?c: Mixed Mode
6. T?i **SQL Server Management Studio (SSMS)** t?: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

#### Trên Linux (Docker)
```bash
# Cài Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sudo sh get-docker.sh

# Ch?y SQL Server
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your@Password123" \
  -p 1433:1433 --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2019-latest
```

### B??c 3: Clone Repository

```bash
# T?o th? m?c d? án
mkdir C:\Projects
cd C:\Projects

# Clone repository
git clone https://github.com/yourname/QlyKhachHang.git
cd QlyKhachHang
```

### B??c 4: C?u Hình Connection String

1. M? file `appsettings.json`
2. C?p nh?t connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=QlyKhachHang;User Id=sa;Password=Your@Password123;TrustServerCertificate=true;"
  }
}
```

**L?u ý:**
- `Server`: ??a ch? SQL Server (localhost ho?c IP address)
- `Database`: Tên database (QlyKhachHang)
- `User Id`: Tên ??ng nh?p SA (SQL Admin)
- `Password`: M?t kh?u SA

### B??c 5: Cài ??t Dependencies

```bash
cd QlyKhachHang
dotnet restore
```

### B??c 6: T?o Database

#### Option 1: S? d?ng Entity Framework Migrations
```bash
# C?p nh?t database v?i migrations
dotnet ef database update

# Ki?m tra migrations
dotnet ef migrations list
```

#### Option 2: T?o Manual
1. M? SQL Server Management Studio
2. K?t n?i v?i SQL Server
3. T?o database m?i:
   ```sql
   CREATE DATABASE QlyKhachHang;
   ```
4. Ch?y script SQL t? file `Database/InitialScript.sql`

### B??c 7: Ch?y ?ng D?ng

```bash
cd QlyKhachHang
dotnet run
```

**Output mong ??i:**
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
Application started.
```

### B??c 8: Truy C?p

M? trình duy?t và truy c?p:
```
https://localhost:5001
```

---

## ?? Tài Kho?n M?c ??nh

### Admin
```
Tên ??ng nh?p: admin
Email: admin@shop.com
M?t kh?u: 123456
Vai trò: Admin
```

### Nhân Viên
```
Tên ??ng nh?p: nhanvien
Email: staff@shop.com
M?t kh?u: 123456
Vai trò: Employee
```

### Khách Hàng
```
Tên ??ng nh?p: khachhang1
Email: kh1@gmail.com
M?t kh?u: MatKhauMoi_123
Vai trò: Customer
```

---

## ??? Xây D?ng & Tri?n Khai

### Build D? Án

#### Debug Build
```bash
dotnet build
```

#### Release Build
```bash
dotnet build -c Release
```

### Ch?y Unit Tests
```bash
dotnet test
```

### Publish (T?o Package)

#### Windows
```bash
dotnet publish -c Release -o ./publish
```

#### Linux
```bash
dotnet publish -c Release -r linux-x64 -o ./publish
```

### Tri?n Khai Lên Production

#### Option 1: IIS (Windows)
1. Publish to folder:
   ```bash
   dotnet publish -c Release -o C:\inetpub\wwwroot\QlyKhachHang
   ```
2. M? IIS Manager
3. T?o Application m?i
4. Ch? ??nh Physical Path

#### Option 2: Docker (Recommended)

1. T?o `Dockerfile`:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY publish/ .
EXPOSE 80
CMD ["dotnet", "QlyKhachHang.dll"]
```

2. Build Docker Image:
```bash
docker build -t qlykachhang:1.0 .
```

3. Ch?y Container:
```bash
docker run -d \
  --name qlykachhang \
  -p 80:80 \
  -e ConnectionStrings__DefaultConnection="Server=sqlserver;Database=QlyKhachHang;User Id=sa;Password=Your@Password123;" \
  qlykachhang:1.0
```

#### Option 3: Azure App Service

1. T?o Azure Account: https://azure.microsoft.com/
2. T?o App Service:
   ```bash
   az appservice plan create \
     --name QlyKhachHangPlan \
     --resource-group MyResourceGroup \
     --sku B1 --is-linux
   ```
3. Publish:
   ```bash
   dotnet publish -c Release -o ./publish
   az webapp up --name qlykachhang --runtime "DOTNETCORE|8.0"
   ```

---

## ?? B?o Trì

### Database Migrations

#### Thêm Migration M?i
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

#### Undo Migration
```bash
dotnet ef migrations remove
dotnet ef database update <PreviousMigration>
```

#### Reset Database
```bash
dotnet ef database drop
dotnet ef database update
```

### Sao L?u Database

#### SQL Server Management Studio
1. Right-click Database
2. Ch?n **Tasks** ? **Back Up...**
3. Ch?n location l?u file
4. Click **OK**

#### Backup Script
```sql
BACKUP DATABASE [QlyKhachHang]
TO DISK = 'C:\Backups\QlyKhachHang_@date.bak'
WITH INIT, COMPRESSION
```

### Restore Database
```sql
RESTORE DATABASE [QlyKhachHang]
FROM DISK = 'C:\Backups\QlyKhachHang_20240101.bak'
WITH REPLACE
```

---

## ?? X? Lý S? C?

### L?i 1: "Cannot open database 'QlyKhachHang'"

**Nguyên Nhân:** Database ch?a ???c t?o

**Gi?i Pháp:**
```bash
dotnet ef database update
```

### L?i 2: "A connection was successfully established with the server, but then an error occurred during the login process"

**Nguyên Nhân:** Sai credentials ho?c SQL Server không ch?y

**Gi?i Pháp:**
1. Ki?m tra SQL Server Services:
   ```bash
   Get-Service -Name MSSQLSERVER
   ```
2. Kh?i ??ng SQL Server:
   ```bash
   Start-Service -Name MSSQLSERVER
   ```
3. Ki?m tra connection string

### L?i 3: "Build failed with exit code 1"

**Nguyên Nhân:** Syntax error ho?c missing packages

**Gi?i Pháp:**
```bash
# Xóa bin và obj
rm -r bin obj

# Restore packages
dotnet restore

# Build l?i
dotnet build
```

### L?i 4: Hi?n th? `?` thay vì ti?ng Vi?t

**Nguyên Nhân:** Encoding không ?úng

**Gi?i Pháp:**
- S? d?ng trình duy?t modern (Chrome, Edge, Firefox)
- Ki?m tra charset: UTF-8
- Clear browser cache (Ctrl+Shift+Delete)

---

## ?? Monitoring

### Log Files
```
C:\Users\<Username>\AppData\Local\Temp\QlyKhachHang\
```

### Application Insights (Azure)
```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "YOUR_KEY"
  }
}
```

### Health Check
```bash
# Ki?m tra k?t n?i database
GET /health
```

---

## ?? B?o M?t

### HTTPS
- Localhost s? d?ng SSL certificate t? ký
- Production ph?i s? d?ng Let's Encrypt ho?c CA certificate

### CORS
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
```

### SQL Injection Prevention
- S? d?ng Entity Framework (ORM)
- Tránh raw SQL queries
- Validate input

### XSS Prevention
- S? d?ng Razor @Html.Encode()
- Content Security Policy

---

## ?? Performance Tuning

### Database Optimization
```sql
-- T?o Index
CREATE INDEX IX_Customer_Email ON Customers(Email);
CREATE INDEX IX_Invoice_CustomerId ON Invoices(CustomerId);

-- Xem Query Performance
SET STATISTICS IO ON;
SELECT * FROM Invoices WHERE CustomerId = 1;
SET STATISTICS IO OFF;
```

### Caching
```csharp
services.AddMemoryCache();
services.AddDistributedMemoryCache();
```

### Async/Await
```csharp
public async Task<IActionResult> Index()
{
    var customers = await _context.Customers.ToListAsync();
    return View(customers);
}
```

---

## ?? H??ng D?n Khác

- [User Manual](USER_MANUAL.md) - H??ng d?n s? d?ng
- [Architecture](ARCHITECTURE.md) - Ki?n trúc h? th?ng
- [API Documentation](API_DOCS.md) - Tài li?u API (s?p t?i)

---

## ?? Liên H? H? Tr?

**Email:** support@qlykachhang.com
**Phone:** 1-800-000-0000
**Website:** https://qlykachhang.com

---

**C?p nh?t:** 2024
**Phiên b?n:** 1.0
**Tr?ng thái:** ? Production Ready
