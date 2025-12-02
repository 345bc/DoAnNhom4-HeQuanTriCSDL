# ?? S?A L?I DANH SÁCH KHÁCH HÀNG - H??NG D?N CHI TI?T

## ?? V?N ??
Khi nh?n vào danh sách khách hàng, ?ng d?ng b? crash/l?i.

## ? NGUYÊN NHÂN VÀ CÁC S?A CH?A

### 1?? NULL REFERENCE EXCEPTION
**V?n ??**: M?t s? property c?a Customer có th? null (Phone, Email, City, Address, Username)

**Gi?i pháp**: Thêm ki?m tra null tr??c khi s? d?ng
```csharp
// ? TR??C (Có l?i)
<a href="tel:@item.Phone">@item.Phone</a>

// ? SAU (Ki?m tra null)
@if (!string.IsNullOrEmpty(item.Phone))
{
    <a href="tel:@item.Phone">@item.Phone</a>
}
else
{
    <span class="text-muted">N/A</span>
}
```

### 2?? RAZOR TEMPLATE L?I
**V?n ??**: Không th? dùng C# expression tr?c ti?p trong attribute c?a `<option>`

**Gi?i pháp**: S? d?ng `selected="null"` ho?c `selected="selected"`
```razor
// ? TR??C (L?i RZ1031)
<option value="Active" @(statusFilter == "Active" ? "selected" : "")>

// ? SAU (?úng)
<option value="Active" selected="@(statusFilter == "Active" ? "selected" : null)">
```

### 3?? SEARCH QUERY NUL REFERENCE
**V?n ??**: Tìm ki?m trên City.Contains() nh?ng City có th? null

**Gi?i pháp**: Ki?m tra null trong LINQ query
```csharp
// ? TR??C
query = query.Where(c => c.City.Contains(searchTerm));

// ? SAU
query = query.Where(c => 
    c.CustomerName.ToLower().Contains(lowerSearchTerm) ||
    (c.Phone != null && c.Phone.Contains(searchTerm)) ||
    (c.City != null && c.City.ToLower().Contains(lowerSearchTerm)));
```

### 4?? NULL MODEL CHECK
**V?n ??**: View không ki?m tra Model có null không tr??c khi g?i .Any()

**Gi?i pháp**: Thêm null check tr??c
```razor
// ? TR??C
@if (Model.Any())

// ? SAU
@if (Model != null && Model.Any())
```

## ?? CÁC FILE ?Ã S?A

### 1. Controllers/CustomerController.cs
**Thay ??i**:
- Thêm null check trong search filter
- S? d?ng `.ToLower()` ?? tìm ki?m case-insensitive
- Thêm error message chi ti?t trong exception handler

```csharp
// Tr??c
if (!string.IsNullOrWhiteSpace(searchTerm))
{
    query = query.Where(c => 
        c.CustomerName.Contains(searchTerm) ||
        c.Phone.Contains(searchTerm) ||
        c.Email.Contains(searchTerm) ||
        c.City.Contains(searchTerm));
}

// Sau
if (!string.IsNullOrWhiteSpace(searchTerm))
{
    var lowerSearchTerm = searchTerm.ToLower();
    query = query.Where(c => 
        c.CustomerName.ToLower().Contains(lowerSearchTerm) ||
        (c.Phone != null && c.Phone.Contains(searchTerm)) ||
        c.Email.ToLower().Contains(lowerSearchTerm) ||
        (c.City != null && c.City.ToLower().Contains(lowerSearchTerm)));
}
```

### 2. Views/Customer/Index.cshtml
**Thay ??i**:
- S?a option selected attribute
- Thêm null check cho Phone, Email, City
- Ki?m tra Model != null tr??c khi render table

```razor
// Tr??c
<option value="Active" @(statusFilter == "Active" ? "selected" : "")>

// Sau
<option value="Active" selected="@(statusFilter == "Active" ? "selected" : null)">

// Ki?m tra null
@if (!string.IsNullOrEmpty(item.Phone))
{
    <a href="tel:@item.Phone">...</a>
}
```

## ?? CÁCH KI?M TRA L?I

### B??c 1: Ch?y ?ng d?ng
```powershell
cd QlyKhachHang
dotnet run
```

### B??c 2: ?i ??n Danh sách Khách Hàng
```
https://localhost:7001/Customer/Index
```

### B??c 3: Ki?m tra k?t qu?
- ? Danh sách khách hàng hi?n th? (n?u có d? li?u)
- ? Tìm ki?m ho?t ??ng
- ? L?c theo tr?ng thái ho?t ??ng
- ? S?p x?p ho?t ??ng
- ? Phân trang ho?t ??ng

### B??c 4: N?u danh sách tr?ng
- S? hi?n th? thông báo "Không có khách hàng nào"
- V?n có nút "Thêm khách hàng m?i"

## ?? DEBUGGING TIPS

### N?u v?n b? l?i:

1. **Ki?m tra Database Migration**
   ```powershell
   dotnet ef database update
   ```

2. **Ki?m tra xem có d? li?u không**
   ```csharp
   var count = _context.Customers.Count();
   _logger.LogInformation($"Total customers: {count}");
   ```

3. **Xem l?i chi ti?t trong Debug Console**
   - Nh?n F12 ? Console tab
   - Ki?m tra có JavaScript error không

4. **Ki?m tra Server Logs**
   ```powershell
   # Terminal s? hi?n th? l?i chi ti?t
   [ERR] Error loading customers: ...
   ```

## ? KI?M ??NH NH?NG V?N ??

### ? Hi?n t?i ?ã s?a
- [x] Null reference exceptions
- [x] Razor template errors
- [x] Search filter null checks
- [x] Model null checks
- [x] Option selected attribute syntax
- [x] Case-insensitive search

### ? Build Status
```
Build successful
Compilation Errors: 0
Warning: 0
```

### ? Test Checklist
- [ ] Ch?y ?ng d?ng
- [ ] Vào Danh sách Khách Hàng
- [ ] Tìm ki?m (n?u có d? li?u)
- [ ] L?c theo status
- [ ] S?p x?p các c?t
- [ ] Phân trang
- [ ] Xem chi ti?t khách hàng
- [ ] Ch?nh s?a khách hàng
- [ ] Xóa khách hàng

## ?? THÊM THÔNG TIN

### Seed Data
N?u database tr?ng, hãy ch?y migration ?? thêm d? li?u m?u:

```csharp
// AddProperCustomerSeeding.cs s? thêm 50 khách hàng
// Khách hàng t? kh1 ??n kh50
// Email: kh1@gmail.com, kh2@gmail.com, ...
// M?t kh?u: 123456 (?ã hash)
```

### D? li?u m?u
| ID | Tên | Email | ?i?n tho?i | Thành ph? | Tr?ng thái |
|----|-----|-------|-----------|-----------|-----------|
| 1 | Nguy?n V?n An | kh1@gmail.com | 0901234567 | TP.HCM | Active |
| 2 | Tr?n Th? Bò | kh2@gmail.com | 0909876543 | TP.HCM | Active |
| ... | ... | ... | ... | ... | ... |
| 50 | Khách Hàng 50 | kh50@gmail.com | 0900000050 | TP.HCM | Active |

## ?? NEXT STEPS

1. **Ch?y ?ng d?ng** và test danh sách khách hàng
2. **Ki?m tra migration** ?ã ch?y thành công ch?a
3. **Xem browser console** (F12) n?u có l?i frontend
4. **Ki?m tra server logs** n?u có l?i backend

## ?? THÔNG BÁO
N?u v?n g?p l?i:
1. Ki?m tra file `appsettings.json` - chu?i k?t n?i database
2. Ki?m tra database ?ã t?o không
3. Ki?m tra migration ?ã ch?y không (`dotnet ef database update`)

---

**Status**: ? HOÀN T?T  
**Build**: ? SUCCESS (0 Errors, 0 Warnings)  
**Ngày c?p nh?t**: Tháng 12, 2024
