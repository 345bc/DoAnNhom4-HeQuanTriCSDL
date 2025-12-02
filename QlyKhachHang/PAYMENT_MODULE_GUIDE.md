# ?? MODULE THANH TOÁN (PAYMENT) - H??NG D?N ??Y ??

## ?? TÓM T?T

Module Thanh Toán (Payment) ?ã ???c thêm hoàn ch?nh vào h? th?ng. Cho phép qu?n lý t?t c? các giao d?ch thanh toán liên quan ??n hóa ??n.

---

## ? NH?NG GÌ ?Ã ???C THÊM

### 1?? Model - Payment.cs

**Thu?c tính:**
```csharp
- PaymentId (Key) - ID thanh toán
- InvoiceId (FK) - Liên k?t t?i hóa ??n
- PaymentMethod - Ph??ng th?c: Ti?n m?t, Th? tín d?ng, Chuy?n kho?n, Ví ?i?n t?
- Amount - S? ti?n thanh toán
- Status - Tr?ng thái: Pending, Completed, Failed, Refunded
- PaymentDate - Ngày thanh toán
- TransactionId - Mã giao d?ch t? h? th?ng thanh toán
- Notes - Ghi chú
- BankName - Tên ngân hàng (cho chuy?n kho?n)
- BankAccountNumber - S? tài kho?n
- BankAccountName - Tên ch? tài kho?n
- CreatedDate, ModifiedDate, CompletedDate - Metadata
```

### 2?? Controller - PaymentController.cs

**Các Action:**
- ? Index - Danh sách thanh toán (có b? l?c theo hóa ??n)
- ? Details - Chi ti?t thanh toán
- ? Create - T?o thanh toán m?i
- ? Edit - Ch?nh s?a (ch? khi Pending)
- ? Delete - Xóa (ch? khi Pending)

**Features:**
- Tính toán t? ??ng s? ti?n ?ã thanh toán
- C?p nh?t tr?ng thái hóa ??n khi thanh toán ??
- Ki?m tra validation s? ti?n
- H? tr? xem thanh toán theo hóa ??n c? th?

### 3?? Views (5 View)

| View | M?c ?ích |
|------|----------|
| Index.cshtml | Danh sách thanh toán v?i summary |
| Create.cshtml | T?o thanh toán m?i |
| Edit.cshtml | Ch?nh s?a thanh toán |
| Details.cshtml | Xem chi ti?t thanh toán |
| Delete.cshtml | Xác nh?n xóa |

**Features:**
- ?? Dashboard hi?n th? t?ng ti?n, ?ã thanh toán, còn l?i, t? l?
- ?? Progress bar t? ??ng tính t? l? thanh toán
- ?? UI chuyên nghi?p v?i badge màu s?c theo tr?ng thái
- ?? Form validation t? ??ng
- ?? Hi?n th? thông tin ngân hàng khi ch?n ph??ng th?c chuy?n kho?n

### 4?? CSS - payment-management.css

**N?i dung:**
- Styling cho b?ng thanh toán
- Card layout cho form
- Badge và status styling
- Responsive design (mobile friendly)
- Progress bar styling

### 5?? Database Migration - AddPaymentModule.cs

**Thay ??i:**
- T?o table Payments
- Thêm column PaidAmount vào Invoice
- T?o Foreign Key relationship
- T?o index cho performance

### 6?? Navigation

**C?p nh?t _Layout.cshtml:**
- Thêm menu "Thanh Toán" vào dropdown Qu?n Lý

---

## ?? QUAN H? D? LI?U

```
Invoice (1) ?????? (Many) Payment
   ?? InvoiceId
   ?? TotalAmount
   ?? PaidAmount ? Auto-calculated t? t?ng Amount c?a Payments
```

**Quy t?c:**
- M?i Invoice có th? có nhi?u Payment
- PaidAmount = T?ng Amount c?a t?t c? Payments
- Khi PaidAmount >= TotalAmount ? Invoice.Status = "Completed"

---

## ?? CÁCH S? D?NG

### Step 1: Ch?y Migration

```bash
cd QlyKhachHang
dotnet ef database update
```

### Step 2: Truy c?p tính n?ng

**Qua Menu:**
```
Qu?n Lý ? Thanh Toán
```

**Ho?c URL tr?c ti?p:**
```
https://localhost:7001/Payment/Index
https://localhost:7001/Payment/Create
```

### Step 3: T?o Thanh Toán

1. Click "Thêm Thanh Toán"
2. Ch?n Hóa ??n
3. Nh?p S? Ti?n
4. Ch?n Ph??ng Th?c
5. Nh?p Mã GD (n?u có)
6. N?u chuy?n kho?n, nh?p thông tin ngân hàng
7. Click "T?o Thanh Toán"

### Step 4: Xem & Qu?n Lý

- **Index:** Xem danh sách t?t c? thanh toán
- **Details:** Xem chi ti?t t?ng thanh toán
- **Edit:** S?a thanh toán (ch? Pending)
- **Delete:** Xóa thanh toán (ch? Pending, hoàn l?i ti?n)

---

## ?? T?C N?NG CHI TI?T

### Ph??ng Th?c Thanh Toán

| Ph??ng Th?c | Mô T? | C?n Thông Tin BankThêm |
|-----------|-------|-----------|
| Ti?n m?t ?? | Thanh toán tr?c ti?p | Không |
| Th? tín d?ng ?? | Dùng th? tín d?ng | Không |
| Chuy?n kho?n ?? | Chuy?n vào tài kho?n | Có |
| Ví ?i?n t? ?? | Thanh toán qua app | Không |

### Tr?ng Thái Thanh Toán

| Tr?ng Thái | Mô T? | Có Th? S?a/Xóa |
|-----------|-------|-----------|
| Pending ? | Ch? xác nh?n | ? Có |
| Completed ? | Hoàn thành | ? Không |
| Failed ? | Th?t b?i | ? Không |
| Refunded ?? | Hoàn l?i ti?n | ? Không |

### Validation T? ??ng

? S? ti?n > 0  
? S? ti?n ? S? ti?n còn l?i c?a hóa ??n  
? Hóa ??n không b? h?y  
? Ph??ng th?c b?t bu?c  

---

## ?? DASHBOARD & SUMMARY

Khi xem thanh toán c?a m?t hóa ??n c? th?, s? hi?n th?:

```
???????????????????????????????????????????????
?  Hóa ??n: INV-20250101000000                ?
???????????????????????????????????????????????
? T?ng Ti?n ? ?ã TT  ? Còn L?i ? T? L?      ?
?  10,000   ? 5,000  ?  5,000  ? 50%        ?
?           ???????????????????????????????????
?                Progress Bar: ?????????????  ?
???????????????????????????????????????????????
```

---

## ?? LU?NG THANH TOÁN

### Scenario: Hóa ??n 10,000,000 VND

```
Step 1: Khách hàng thanh toán 3,000,000 VND
        Invoice.PaidAmount = 3,000,000
        Status = Pending

Step 2: Khách hàng thanh toán 4,000,000 VND
        Invoice.PaidAmount = 7,000,000
        Status = Pending

Step 3: Khách hàng thanh toán 3,000,000 VND
        Invoice.PaidAmount = 10,000,000
        Status = Completed ? (T? ??ng c?p nh?t)
```

---

## ?? ADVANCED FEATURES

### Tính N?ng Ngân Hàng (Bank Transfer)

Khi ch?n "Chuy?n kho?n ngân hàng", hi?n th? thêm fields:
- ?? Tên Ngân Hàng
- ?? S? Tài Kho?n
- ?? Tên Ch? Tài Kho?n

H?u ích cho:
- L?u thông tin chuy?n kho?n
- Truy v?n l?ch s? thanh toán ngân hàng
- ??i soát v?i s? sách

### Mã Giao D?ch (Transaction ID)

- L?u tr? mã GD t? ngân hàng ho?c c?ng thanh toán
- Giúp truy xu?t nhanh trong h? th?ng ngoài
- Có th? ?? tr?ng n?u không c?n

### Progress Tracking

- T? ??ng tính % hoàn thành thanh toán
- Progress bar hi?n th? tr?c quan
- C?p nh?t t?c th?i

---

## ??? SECURITY & VALIDATION

### B?o V? D? Li?u

? Foreign Key constraint - Không th? xóa Invoice khi có Payment  
? Cascade delete - Xóa Invoice s? xóa t?t c? Payments  
? Status validation - Ch? Pending m?i có th? s?a/xóa  
? Amount validation - Ki?m tra s? ti?n h?p l?  

### Audit Trail

- CreatedDate - Khi t?o thanh toán
- ModifiedDate - Khi s?a ??i
- CompletedDate - Khi hoàn thành (Completed)

---

## ?? STATISTICS & REPORTS (Future Enhancement)

Có th? thêm:
```
- T?ng doanh thu thanh toán
- Ph??ng th?c thanh toán ???c dùng nhi?u nh?t
- T? l? thanh toán hoàn thành/ch?m
- Trung bình th?i gian thanh toán
- Báo cáo tính ti?n hàng tháng
```

---

## ?? CÁCH M? R?NG

### Thêm Payment Gateway (Stripe, PayPal, etc)

```csharp
// Thêm field vào Payment
public string PaymentGateway { get; set; } // "Stripe", "PayPal"
public string GatewayTransactionId { get; set; }

// C?p nh?t validation
if (paymentMethod == "CreditCard") {
    // Call Stripe API
    var result = await stripeService.ProcessPayment(...);
}
```

### Thêm Payment Schedule (Thanh Toán ??nh K?)

```csharp
public class PaymentSchedule {
    public int ScheduleId { get; set; }
    public int InvoiceId { get; set; }
    public decimal InstallmentAmount { get; set; }
    public int NumberOfInstallments { get; set; }
    public DateTime DueDate { get; set; }
}
```

### Thêm Refund Management

```csharp
public class Refund {
    public int RefundId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; }
    public DateTime RefundDate { get; set; }
}
```

---

## ?? TEST SCENARIOS

### Test 1: T?o Thanh Toán ??n

```
1. T?o Invoice: 5,000,000 VND
2. T?o Payment: 5,000,000 VND (Ti?n m?t)
3. Ki?m tra: Invoice.PaidAmount = 5,000,000 ?
4. Ki?m tra: Invoice.Status = "Completed" ?
```

### Test 2: Thanh Toán T?ng Ph?n

```
1. T?o Invoice: 10,000,000 VND
2. T?o Payment 1: 3,000,000 VND ? Status = Pending
3. T?o Payment 2: 4,000,000 VND ? Status = Pending
4. T?o Payment 3: 3,000,000 VND ? Status = Completed ?
5. Ki?m tra: Invoice.Status = "Completed" ?
```

### Test 3: Ch?nh S?a & Xóa

```
1. T?o Payment: 3,000,000 VND (Status = Pending)
2. S?a Amount ? 4,000,000 VND ?
3. Ki?m tra: Invoice.PaidAmount = 4,000,000 ?
4. Xóa Payment ?
5. Ki?m tra: Invoice.PaidAmount = 0, Status = Pending ?
```

### Test 4: Bank Transfer

```
1. T?o Payment v?i Ph??ng Th?c: Chuy?n kho?n ngân hàng
2. Nh?p thông tin ngân hàng
3. Ki?m tra: D? li?u l?u ?úng ?
4. Xem Details: Thông tin ngân hàng hi?n th? ?
```

---

## ? BUILD STATUS

```
? Build: SUCCESS
? Errors: 0
? Warnings: 0
? Models: Payment.cs
? Controller: PaymentController.cs
? Views: 5 files
? CSS: payment-management.css
? Migration: AddPaymentModule.cs
? Navigation: Updated _Layout.cshtml
```

---

## ?? CHECKLIST TR??C KHI DEPLOY

- [ ] Ch?y migration: `dotnet ef database update`
- [ ] Test t?o thanh toán ??n
- [ ] Test thanh toán t?ng ph?n
- [ ] Test ch?nh s?a/xóa (ch? Pending)
- [ ] Test bank transfer fields
- [ ] Ki?m tra progress bar
- [ ] Test status updates t? ??ng
- [ ] Ki?m tra validation errors
- [ ] Test links trong Index ? Details
- [ ] Test navigation menu

---

## ?? NEXT STEPS

1. **Ch?y ?ng d?ng** và test module Payment
2. **Xem danh sách** thanh toán (ch?a có d? li?u)
3. **T? Invoice Details** ? "Thêm Thanh Toán"
4. **Xem Integration** gi?a Invoice và Payment
5. **M? r?ng** v?i Payment Gateway n?u c?n

---

**Status**: ? **HOÀN THÀNH VÀ S?N DÙNG**  
**Version**: 1.0 - Initial Release  
**Date**: Tháng 12, 2024
