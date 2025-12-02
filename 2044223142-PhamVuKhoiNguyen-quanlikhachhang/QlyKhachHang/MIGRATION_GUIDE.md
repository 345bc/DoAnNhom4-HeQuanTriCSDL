# 📊 MIGRATION DATA SEED GUIDE

## Tổng Quan

Project hiện có **3 Migration Files** chứa seed data hoàn chỉnh:

### 1️⃣ **AddAuthenticationFields.cs** (Migration gốc)
- **Mục đích:** Tạo cấu trúc database và các bảng chính
- **Dữ liệu:** Trống (không chứa InsertData)
- **Bảng được tạo:**
  - Customers (50 trống, sẽ được thêm ở migration khác)
  - Products (50 trống, sẽ được thêm ở migration khác)
  - Carts
  - CartItems
  - Invoices
  - InvoiceDetails
  - Reviews

---

### 2️⃣ **AddSeedData.cs** (Migration thứ 2)
- **Mục đích:** Thêm dữ liệu sản phẩm và khách hàng cơ bản
- **Dữ liệu được thêm:**
  - ✅ **50 Products** - Sản phẩm thể thao từ các brand (Nike, Adidas, Puma, Reebok, New Balance)
  - ✅ **50 Customers** - Khách hàng với thông tin cá nhân, ngày sinh, tỏa địa chỉ

**Các trường dữ liệu:**
```
Products:
- ProductId (1-50)
- ProductName: Áo Nike Air, Quần Adidas Training, ...
- Description: Mô tả sản phẩm
- Price: 50,000₫ - 820,000₫
- Stock: 45-410 sản phẩm
- Category: Áo, Quần, Giày, Mũ, Tất, Áo Khoác, Thắt Lưng, Ba Lô, Dây Giày, Túi
- CreatedDate: 2025-11-24

Customers:
- CustomerId (1-50)
- CustomerName: Nguyễn Văn An, Trần Thị Bò, ...
- Email: kh1@gmail.com - kh50@gmail.com
- Phone: 0901234567 - 0940404040
- Address: 123 Đường 1-50, TP.HCM
- City: TP.HCM
- PostalCode: 70000
- BirthDate: 1988-1999
- Username: kh1 - kh50
- PasswordHash: Mã hóa SHA256
- Status: Active
- CreatedDate: 2025-11-24
```

---

### 3️⃣ **AddCompleteBusinessData.cs** (Migration thứ 3) ⭐ **NEW**
- **Mục đích:** Thêm dữ liệu giao dịch hoàn chỉnh
- **Dữ liệu được thêm:**
  - ✅ **50 Invoices** - Đơn hàng/hóa đơn
  - ✅ **50 InvoiceDetails** - Chi tiết từng dòng hóa đơn

**Các trường dữ liệu:**
```
Invoices:
- InvoiceId (1-50)
- CustomerId (1-25, có lặp lại)
- InvoiceNumber: ORD001 - ORD050
- InvoiceDate: 2025-11-20 đến 2025-11-24
- SubTotal: 350,000₫ - 1,000,000₫
- TotalAmount: 350,000₫ - 1,100,000₫ (có thuế/phí)
- ShippingAddress: Các địa chỉ TP.HCM và Hà Nội
- Status: Đã giao, Đang giao, Chờ xác nhận, Hủy
- CreatedDate: Ngày tạo hóa đơn

InvoiceDetails:
- InvoiceDetailId (1-50)
- InvoiceId (1-50)
- ProductId (1-10, có lặp lại)
- Quantity: 1-3 sản phẩm
- UnitPrice: Giá đơn vị từ 250,000₫ - 750,000₫
```

---

## 📋 Thứ Tự Áp Dụng Migrations

```bash
# Bước 1: Tạo database structure
dotnet ef database update AddAuthenticationFields

# Bước 2: Thêm 50 Products và 50 Customers
dotnet ef database update AddSeedData

# Bước 3: Thêm 50 Invoices và 50 InvoiceDetails
dotnet ef database update AddCompleteBusinessData

# Hoặc update tất cả cùng lúc
dotnet ef database update
```

---

## 📊 Dữ Liệu Đã Thêm - Tóm Tắt

| Entity | Số lượng | Status |
|--------|----------|--------|
| **Customers** | 50 | ✅ Active |
| **Products** | 50 | ✅ Complete |
| **Invoices** | 50 | ✅ Mixed Status |
| **InvoiceDetails** | 50 | ✅ Complete |
| **Carts** | Chưa thêm | ⏳ Pending |
| **CartItems** | Chưa thêm | ⏳ Pending |
| **Reviews** | Chưa thêm | ⏳ Pending |

---

## 🔄 Thống Kê Dữ Liệu

### Invoices Status Distribution
- ✅ **Đã giao:** ~20 đơn (40%)
- ⏳ **Đang giao:** ~15 đơn (30%)
- ⏸️ **Chờ xác nhận:** ~10 đơn (20%)
- ❌ **Hủy:** ~5 đơn (10%)

### Products by Category
- **Áo:** 10 sản phẩm (IDs: 1-5, 26-30)
- **Quần:** 5 sản phẩm (IDs: 6-10)
- **Giày:** 5 sản phẩm (IDs: 11-15)
- **Mũ:** 5 sản phẩm (IDs: 16-20)
- **Tất:** 5 sản phẩm (IDs: 21-25)
- **Áo Khoác:** 5 sản phẩm (IDs: 26-30)
- **Thắt Lưng:** 5 sản phẩm (IDs: 31-35)
- **Ba Lô:** 5 sản phẩm (IDs: 36-40)
- **Dây Giày:** 5 sản phẩm (IDs: 41-45)
- **Túi:** 5 sản phẩm (IDs: 46-50)

### Customers Distribution
- **Từ Khách 1-25:** Được lặp lại trong 50 Invoices
- **Phân bổ địa chỉ:** TP.HCM (Khách 1-20), Hà Nội (Khách 21-25+)
- **Giới tính:** Nam/Nữ xen kẽ (50/50)

---

## 💾 Cách Xóa Dữ Liệu

Nếu cần rollback:

```bash
# Xóa tất cả seed data - quay lại trạng thái database trống
dotnet ef database update AddAuthenticationFields

# Xóa toàn bộ database
dotnet ef database drop

# Tạo lại database từ đầu
dotnet ef database update
```

---

## 🚀 Tiếp Theo

Để hoàn chỉnh, cần thêm:
1. **50 Carts** - Giỏ hàng của 50 khách hàng
2. **~100-150 CartItems** - Chi tiết giỏ hàng
3. **~100-150 Reviews** - Đánh giá sản phẩm

---

## ✅ Build Status

- ✅ AddAuthenticationFields - SUCCESS
- ✅ AddSeedData - SUCCESS
- ✅ AddCompleteBusinessData - SUCCESS

**Total Data Seed:** 150+ records
**Total Migration Files:** 3
**Status:** Ready to Deploy

---

*Generated: 2025-11-24*
*Last Updated: AddCompleteBusinessData Migration*
