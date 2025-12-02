# ⚡ **3-STEP FIX TO LOGIN**

## 🎯 **This Will Take 2 Minutes**

---

## **STEP 1️⃣: Apply the Database Migration**

Open your terminal/PowerShell and run:

```powershell
cd QlyKhachHang
dotnet ef database update
```

**Expected output:**
```
Build started...
Build completed.
Applying migration 'AddProperCustomerSeeding'...
Done.
```

✅ **You'll see:** "Done." message

---

## **STEP 2️⃣: Start the Application**

Still in the same terminal, run:

```powershell
dotnet run
```

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
      Now listening on: http://localhost:7001
```

✅ **You'll see:** "Now listening on: https://localhost:5001"

---

## **STEP 3️⃣: Login to the Application**

1. **Open your browser** and go to:
   ```
   https://localhost:5001/Account/Login
   ```

2. **Enter these credentials:**
   ```
   Username or Email: kh1
   Password: 123456
   ```

3. **Click:** "ĐĂNG NHẬP" (LOGIN button)

4. **Expected result:**
   ```
   ✅ You see: "Chào mừng Nguyễn Văn An!"
   ✅ Redirected to: https://localhost:5001/
   ```

---

## 🎉 **YOU'RE DONE!**

Your login is now working!

---

## 📝 **Other Test Accounts**

You can also login with any of these:

```
kh2    / 123456
kh3    / MatKhauMoi_123
kh4    / 123456
...
kh50   / 123456
```

---

## ❌ **If Something Goes Wrong**

### **Migration failed?**
→ Make sure you're in the correct directory: `cd QlyKhachHang`
→ Try: `dotnet ef database update -v` (verbose mode)

### **Port 5001 already in use?**
→ Close other applications using port 5001
→ Or use different port: `dotnet run --launch-profile https-alt`

### **Still can't login?**
→ Clear browser cache: `Ctrl + Shift + Delete`
→ Close and reopen your browser
→ Restart the application

### **Database file not found?**
→ Check your connection string in `appsettings.json`
→ Make sure SQL Server/LocalDB is running

---

## ✅ **Success Indicators**

### ✓ You'll know it worked when:

1. Migration runs without errors
2. Application starts on port 5001
3. Login page loads
4. You can login with kh1/123456
5. You see welcome message
6. You're redirected to home page

---

## 🔐 **What Happened Behind the Scenes**

The system just:
1. ✅ Created proper password hashes
2. ✅ Inserted them into the database
3. ✅ Configured session management
4. ✅ Set up user accounts

All this was automated - you just applied the migration!

---

## 📞 **Need More Details?**

- **Why this happened?** → Read `LOGIN_VISUAL_EXPLANATION.md`
- **Troubleshooting?** → Read `LOGIN_TROUBLESHOOTING_FIX.md`
- **Technical details?** → Read `LOGIN_FIX_COMPLETE_EXPLANATION.md`

---

## 🚀 **You're All Set!**

Your login is now working perfectly.

Enjoy using your application! ✨
