# 📋 HƯỚNG DẪN ĐĂNG NHẬP HỆ THỐNG

## ✅ Tài khoản test sẵn có

### 1️⃣ Admin
- **Tên đăng nhập:** `admin`
- **Email:** `admin@shop.com`
- **Mật khẩu:** `123456`
- **Quyền:** Admin

### 2️⃣ Nhân viên
- **Tên đăng nhập:** `nhanvien`
- **Email:** `staff@shop.com`
- **Mật khẩu:** `123456`
- **Quyền:** Employee

### 3️⃣ Khách hàng 1
- **Tên đăng nhập:** `khachhang1`
- **Email:** `kh1@gmail.com`
- **Mật khẩu:** `MatKhauMoi_123`
- **Quyền:** Customer

### 4️⃣ Khách hàng 2
- **Tên đăng nhập:** `khachhang2`
- **Email:** `kh2@gmail.com`
- **Mật khẩu:** `123456`
- **Quyền:** Customer

---

## 🔐 Cách đăng nhập

1. **Truy cập trang đăng nhập:** `https://localhost:5001/Account/Login`

2. **Chọn một trong hai cách:**
   - Nhập **tên đăng nhập** (ví dụ: `admin`, `khachhang1`)
   - HOẶC nhập **email** (ví dụ: `admin@shop.com`, `kh1@gmail.com`)

3. **Nhập mật khẩu** - Sử dụng nút **👁️ (eye icon)** để xem/ẩn mật khẩu

4. **Nhấp "ĐĂNG NHẬP"**

---

## ⚙️ Tính năng mới

✅ **Hiện/Ẩn mật khẩu** - Click nút mắt bên phải input password
✅ **Lưu mật khẩu tạm thời** - Nếu có lỗi, mật khẩu sẽ được khôi phục
✅ **Tìm kiếm linh hoạt** - Dùng tên đăng nhập HOẶC email
✅ **Thông báo lỗi rõ ràng** - Hiển thị lỗi chi tiết nếu đăng nhập thất bại

---

## 🛠️ Các vấn đề đã sửa

| Vấn đề | Giải pháp |
|--------|----------|
| ❌ Mật khẩu bị văng | ✅ Lưu vào sessionStorage khi nhập |
| ❌ Không có nút show password | ✅ Thêm nút 👁️ để toggle |
| ❌ Không hỗ trợ username | ✅ Thêm Username field + tìm kiếm |
| ❌ Lỗi validation không rõ | ✅ Hiển thị thông báo chi tiết |

---

## 📞 Liên hệ hỗ trợ

Nếu gặp vấn đề, vui lòng:
1. Kiểm tra lại tên đăng nhập/email và mật khẩu
2. Chắc chắn Database đã được update với migration
3. Xem logs trong Application Output
