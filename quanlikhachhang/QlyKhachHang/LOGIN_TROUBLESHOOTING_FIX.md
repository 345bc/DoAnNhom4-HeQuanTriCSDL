# 🔐 LOGIN TROUBLESHOOTING & FIX GUIDE

## 🔴 **WHY YOU CAN'T LOGIN - ROOT CAUSE ANALYSIS**

### **The Problem:**
You have a **mismatch between password hashing and seed data**:

1. **Database Issue:**
   - Customers table has invalid password hash: `ICy5YqxTtJm7K2hl3dqYvIcUkIjOiknKYvTmztqTvzc=`
   - This hash doesn't correspond to any plaintext password used in the system

2. **Authentication Service Issue:**
   - Uses SHA256 algorithm to hash passwords
   - When user enters "123456", it hashes to: `jZae727K08KaOmKSgOaGzww/XVqGr58EV4d0p0bQ4O4=`
   - But the stored hash is: `ICy5YqxTtJm7K2hl3dqYvIcUkIjOiknKYvTmztqTvzc=` ❌ **NO MATCH!**

3. **Result:**
   - Login always fails with: "Tên đăng nhập/email hoặc mật khẩu không chính xác"

---

## ✅ **SOLUTION: 3 STEPS TO FIX**

### **Step 1: Apply the New Migration**

Run this command to update the database with properly hashed passwords:

```powershell
cd QlyKhachHang
dotnet ef migrations add AddProperCustomerSeeding
dotnet ef database update
```

### **Step 2: Now You Can Login With:**

| Username | Email | Password |
|----------|-------|----------|
| `kh1` | `kh1@gmail.com` | `123456` ✅ |
| `kh2` | `kh2@gmail.com` | `123456` ✅ |
| `kh3` | `kh3@gmail.com` | `MatKhauMoi_123` ✅ |
| `kh4` | `kh4@gmail.com` | `123456` ✅ |
| `kh5-kh50` | `kh[N]@gmail.com` | `123456` ✅ |

### **Step 3: Test the Login**

1. Go to: `https://localhost:5001/Account/Login`
2. Enter: `kh1` (username)
3. Enter: `123456` (password)
4. Click: **ĐĂNG NHẬP**
5. You should be redirected to Home page ✅

---

## 🔍 **WHY THIS HAPPENED**

### **The Database Schema Issue:**

Your system has **TWO separate authentication models**:

```
┌──────────────────────────┐
│      User Table          │  ← Stores plaintext passwords
├──────────────────────────┤
│ UserID | Name | Password │
│ 1      | admin | 123456   │
└──────────────────────────┘
              ✗ NOT USED FOR LOGIN

┌────────────────────────────────────┐
│      Customer Table                │  ← Used by Login
├────────────────────────────────────┤
│ CustomerId | Username | PasswordHash │
│ 1          | kh1      | ????? (BAD)   │
└────────────────────────────────────┘
              ✓ THIS IS USED FOR LOGIN
```

### **The Previous Issue:**
The migration stored an **arbitrary hash** that doesn't match any actual password.

### **The Fix:**
We now seed with **proper SHA256 hashes** that match the passwords:
- `"123456"` → SHA256 → `"jZae727K08KaOmKSgOaGzww/XVqGr58EV4d0p0bQ4O4="`
- These hashes are now properly stored in the database

---

## 🛠️ **IF YOU STILL CAN'T LOGIN**

### **Check 1: Database Migration Applied**
```powershell
# Verify migrations
dotnet ef migrations list

# Should show: AddProperCustomerSeeding (latest)
```

### **Check 2: Verify Database Content**
```sql
-- Run in SQL Server Management Studio
SELECT CustomerId, Username, Email, PasswordHash, Status 
FROM Customers 
WHERE CustomerId = 1;

-- Should return:
-- CustomerId: 1
-- Username: kh1
-- Email: kh1@gmail.com
-- Status: Active
```

### **Check 3: Check Application Logs**
- Look at browser console (F12 → Console tab)
- Check application output logs
- Search for: "Login attempt failed"

### **Check 4: Clear Browser Cache**
```
Ctrl + Shift + Delete  (on Windows)
Command + Shift + Delete  (on Mac)
```

### **Check 5: Restart Application**
```powershell
# Stop the app (Ctrl + C)
dotnet run
# Try login again
```

---

## 📋 **VERIFICATION CHECKLIST**

- [ ] Applied new migration: `AddProperCustomerSeeding`
- [ ] Database updated successfully
- [ ] Customers table has Username and PasswordHash fields
- [ ] Password hashes look valid (long Base64 strings)
- [ ] Customer statuses are "Active"
- [ ] AuthenticationService.cs has correct VerifyPassword logic
- [ ] Browser cache cleared
- [ ] Application restarted

---

## 🔐 **HOW PASSWORD VERIFICATION WORKS**

```
User enters: "123456"
       ↓
AuthenticationService.LoginAsync("kh1", "123456")
       ↓
HashPassword("123456") → SHA256 → "jZae727K08KaOmKSgOaGzww/XVqGr58EV4d0p0bQ4O4="
       ↓
Compare with stored: "jZae727K08KaOmKSgOaGzww/XVqGr58EV4d0p0bQ4O4="
       ↓
✅ MATCH → Login Success
❌ NO MATCH → Login Failed
```

---

## 📊 **PASSWORD HASH REFERENCE**

| Password | SHA256 Hash (Base64) |
|----------|---------------------|
| `123456` | `jZae727K08KaOmKSgOaGzww/XVqGr58EV4d0p0bQ4O4=` |
| `MatKhauMoi_123` | `ICy5YqxTtJm7K2hl3dqYvIcUkIjOiknKYvTmztqTvzc=` |

---

## ⚠️ **IMPORTANT NOTES**

1. **User Table vs Customer Table:**
   - `User` table = NOT used for customer login (legacy data)
   - `Customer` table = **USED for login** ✓

2. **Password Hashing:**
   - All passwords are hashed with SHA256
   - Never stored as plaintext
   - Verified using `VerifyPassword()` method

3. **Session Management:**
   - Session stored: CustomerId, CustomerName, CustomerEmail
   - Session timeout: 30 minutes
   - Session required for access to: Profile, Orders, etc.

4. **Error Messages:**
   - "Tên đăng nhập/email hoặc mật khẩu không chính xác" = Either username/email doesn't exist OR password is wrong

---

## 🎯 **NEXT STEPS**

1. ✅ Apply migration
2. ✅ Test login with `kh1` / `123456`
3. ✅ Verify session is created (check browser storage)
4. ✅ Access protected pages (Profile, Orders)
5. ✅ Test logout

**Status: Ready to Fix** ✓
