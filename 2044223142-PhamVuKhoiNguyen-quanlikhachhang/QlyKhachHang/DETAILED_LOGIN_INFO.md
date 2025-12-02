# ?? THÔNG TIN ??NG NH?P - CHI TI?T ??Y ??

## ?? L?U Ý QUAN TR?NG

Database ch? l?u tr? **PasswordHash** (mã hóa SHA256), không l?u plaintext password.

Tuy nhiên, **password g?c (plaintext) là: `123456789`** cho t?t c? 50 tài kho?n

---

## ?? TÀI KHO?N ??NG NH?P

### **Thông Tin G?c (Plaintext)**
```
Username:  kh1 (ho?c email: kh1@gmail.com)
Password:  123456789  ? Password G?CN (plaintext)
```

### **Thông Tin Trong Database**
```
Database Column: Username = "kh1"
Database Column: PasswordHash = "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k"
                 ? ?ây là SHA256 hash c?a password "123456789"
```

---

## ?? DANH SÁCH 50 TÀI KHO?N HOÀN CH?NH

### ??nh D?ng: Username | Email | Password (g?c)

```
kh1    | kh1@gmail.com    | 123456789
kh2    | kh2@gmail.com    | 123456789
kh3    | kh3@gmail.com    | 123456789
kh4    | kh4@gmail.com    | 123456789
kh5    | kh5@gmail.com    | 123456789
kh6    | kh6@gmail.com    | 123456789
kh7    | kh7@gmail.com    | 123456789
kh8    | kh8@gmail.com    | 123456789
kh9    | kh9@gmail.com    | 123456789
kh10   | kh10@gmail.com   | 123456789
kh11   | kh11@gmail.com   | 123456789
kh12   | kh12@gmail.com   | 123456789
kh13   | kh13@gmail.com   | 123456789
kh14   | kh14@gmail.com   | 123456789
kh15   | kh15@gmail.com   | 123456789
kh16   | kh16@gmail.com   | 123456789
kh17   | kh17@gmail.com   | 123456789
kh18   | kh18@gmail.com   | 123456789
kh19   | kh19@gmail.com   | 123456789
kh20   | kh20@gmail.com   | 123456789
kh21   | kh21@gmail.com   | 123456789
kh22   | kh22@gmail.com   | 123456789
kh23   | kh23@gmail.com   | 123456789
kh24   | kh24@gmail.com   | 123456789
kh25   | kh25@gmail.com   | 123456789
kh26   | kh26@gmail.com   | 123456789
kh27   | kh27@gmail.com   | 123456789
kh28   | kh28@gmail.com   | 123456789
kh29   | kh29@gmail.com   | 123456789
kh30   | kh30@gmail.com   | 123456789
kh31   | kh31@gmail.com   | 123456789
kh32   | kh32@gmail.com   | 123456789
kh33   | kh33@gmail.com   | 123456789
kh34   | kh34@gmail.com   | 123456789
kh35   | kh35@gmail.com   | 123456789
kh36   | kh36@gmail.com   | 123456789
kh37   | kh37@gmail.com   | 123456789
kh38   | kh38@gmail.com   | 123456789
kh39   | kh39@gmail.com   | 123456789
kh40   | kh40@gmail.com   | 123456789
kh41   | kh41@gmail.com   | 123456789
kh42   | kh42@gmail.com   | 123456789
kh43   | kh43@gmail.com   | 123456789
kh44   | kh44@gmail.com   | 123456789
kh45   | kh45@gmail.com   | 123456789
kh46   | kh46@gmail.com   | 123456789
kh47   | kh47@gmail.com   | 123456789
kh48   | kh48@gmail.com   | 123456789
kh49   | kh49@gmail.com   | 123456789
kh50   | kh50@gmail.com   | 123456789
```

---

## ?? CÓ TH? KI?M TRA TRONG DATABASE

### SQL Query ?? xem d? li?u trong Customers table:

```sql
SELECT 
    CustomerId,
    Username,
    Email,
    CustomerName,
    PasswordHash,
    Status
FROM Customers
WHERE CustomerId <= 10
ORDER BY CustomerId
```

### K?t Qu? Mong ??i:

```
CustomerId | Username | Email        | CustomerName     | PasswordHash (SHA256)         | Status
-----------|----------|--------------|------------------|-------------------------------|---------
1          | kh1      | kh1@gmail.com| Nguy?n V?n An   | j2i+6ZQwCH+qAj7J2C3s7Y5k... | Active
2          | kh2      | kh2@gmail.com| Tr?n Th? Bò      | j2i+6ZQwCH+qAj7J2C3s7Y5k... | Active
3          | kh3      | kh3@gmail.com| Ph?m Minh Tu?n   | j2i+6ZQwCH+qAj7J2C3s7Y5k... | Active
...
```

> **Ghi Chú:** T?t c? 50 tài kho?n ??u có **cùng PasswordHash** vì t?t c? ??u dùng **cùng password: 123456789**

---

## ?? CÁC TR??NG D? LI?U CUSTOMER

```csharp
public class Customer
{
    public int CustomerId { get; set; }           // 1-50
    public string Username { get; set; }          // kh1 - kh50
    public string PasswordHash { get; set; }      // SHA256 hash
    public string Email { get; set; }             // kh1@gmail.com - kh50@gmail.com
    public string CustomerName { get; set; }      // Tên khách hàng
    public string Phone { get; set; }             // S? ?i?n tho?i
    public string Address { get; set; }           // ??a ch?
    public string City { get; set; }              // Thành ph?
    public string PostalCode { get; set; }        // Mã b?u chính
    public string Status { get; set; }            // "Active"
    public DateTime BirthDate { get; set; }       // Ngày sinh
    public string Gender { get; set; }            // "Nam" ho?c "N?"
    public DateTime CreatedDate { get; set; }     // Ngày t?o tài kho?n
}
```

---

## ?? C? TRÌNH KI?M CH?NG M?T KH?U

### Khi b?n ??ng nh?p:

```csharp
// 1. Ng??i dùng nh?p:
string userInput = "123456789";

// 2. H? th?ng hash l?i:
string hashFromInput = HashPassword("123456789");
// ? "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k"

// 3. So sánh v?i database:
if (hashFromInput == customerFromDb.PasswordHash) {
    // ? ??ng nh?p thành công!
}
```

### Mã Hash SHA256:

```
Plaintext:  "123456789"
Algorithm:  SHA256
Hash:       "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k"
```

---

## ?? N?I TÌM THÔNG TIN NÀY TRONG CODE

### File: `QlyKhachHang\Data\ApplicationDbContext.cs`

```csharp
private void SeedData(ModelBuilder modelBuilder)
{
    // Dòng ~315
    var customers = new[]
    {
        new Customer { 
            CustomerId = 1, 
            Username = "kh1",              // ? TÊN ??NG NH?P
            Email = "kh1@gmail.com", 
            CustomerName = "Nguy?n V?n An",
            PasswordHash = "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", // ? M?T KH?U HASH
            Status = "Active",
            // ... các tr??ng khác
        },
        // ... thêm 49 tài kho?n khác
    };
    modelBuilder.Entity<Customer>().HasData(customers);
}
```

---

## ?? CÁCH ??NG NH?P VÀO H? TH?NG

### **B??c 1: Truy c?p trang login**
```
http://localhost:5000/Account/Login
```

### **B??c 2: Nh?p thông tin**

| Tr??ng | Giá Tr? |
|--------|--------|
| **Username ho?c Email** | `kh1` ho?c `kh1@gmail.com` |
| **Password** | `123456789` |

### **B??c 3: Nh?n "??ng Nh?p"**

? H? th?ng s?:
1. Tìm ki?m customer v?i username/email = `kh1`
2. Hash password `123456789` 
3. So sánh hash v?i `PasswordHash` trong database
4. N?u kh?p ? ? ??ng nh?p thành công

---

## ? TÓML??T

| Khía C?nh | Giá Tr? |
|-----------|--------|
| **Username** | kh1 - kh50 |
| **Password (Plaintext)** | 123456789 |
| **Password (Hash SHA256)** | j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k |
| **Email** | kh1@gmail.com - kh50@gmail.com |
| **Status** | Active |
| **T?ng Tài Kho?n** | 50 |

---

**Bây gi? b?n ?ã bi?t:**
- ? Username: `kh1` (và kh2-kh50)
- ? Password: `123456789`
- ? Cách password ???c mã hóa trong database
- ? Cách ki?m tra trong database
