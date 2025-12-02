# ? QUICK START - MODULE THANH TOÁN

## ?? 3 B??c Setup

### Step 1: Migration
```bash
dotnet ef database update
```

### Step 2: Run
```bash
dotnet run
```

### Step 3: Access
```
Menu: Qu?n Lý ? Thanh Toán
URL: https://localhost:7001/Payment/Index
```

---

## ?? Files ???c Thêm

| File | Lo?i | M?c ?ích |
|------|------|----------|
| Models/Payment.cs | C# Model | Data Model |
| Controllers/PaymentController.cs | Controller | Business Logic |
| Views/Payment/Index.cshtml | View | Danh Sách |
| Views/Payment/Create.cshtml | View | T?o M?i |
| Views/Payment/Edit.cshtml | View | Ch?nh S?a |
| Views/Payment/Details.cshtml | View | Chi Ti?t |
| Views/Payment/Delete.cshtml | View | Xóa |
| wwwroot/css/payment-management.css | CSS | Styling |
| Migrations/AddPaymentModule.cs | Migration | Database |

---

## ? FEATURES

? T?o/Xem/S?a/Xóa thanh toán  
? 4 ph??ng th?c thanh toán  
? Auto-update s? ti?n hóa ??n  
? Auto-complete invoice khi ?? ti?n  
? Progress bar dashboard  
? Bank transfer fields  
? Status tracking  
? Full validation  

---

## ?? LIÊN K?T

**T? Invoice:**
```
Invoice Details ? [Thêm Thanh Toán] ? Payment/Create
```

**T? Payment:**
```
Payment Index ? Select ? Payment Details ? [Xem H?]
```

---

## ?? DATABASE

```sql
-- T?o table Payments
CREATE TABLE Payments (
    PaymentId INT PRIMARY KEY,
    InvoiceId INT FOREIGN KEY,
    PaymentMethod NVARCHAR(50),
    Amount DECIMAL(12,2),
    Status NVARCHAR(20),
    ... (thêm 9 c?t khác)
)

-- Thêm column vào Invoice
ALTER TABLE Invoices ADD PaidAmount DECIMAL(12,2)
```

---

## ?? TEST NGAY

**T?o thanh toán:**
1. Vào Invoice ? Chi ti?t
2. Click "Thêm Thanh Toán"
3. Ch?n hóa ??n
4. Nh?p s? ti?n
5. Ch?n ph??ng th?c
6. Click "T?o"

? Xong!

---

**Status**: ? READY
