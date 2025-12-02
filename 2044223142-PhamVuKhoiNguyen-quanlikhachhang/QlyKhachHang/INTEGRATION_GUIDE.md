# ?? H??ng D?n Tích H?p ??ng Nh?p T? ?ng D?ng Web Khác

## T?ng Quan

N?u ph?n ??ng nh?p ?ã ???c x? lý b?i m?t ?ng d?ng web khác, ?ng d?ng này có th? ???c tích h?p theo nh?ng cách sau:

---

## 1?? Single Sign-On (SSO) - ??ng Nh?p T?p Trung

### Khái Ni?m
- Ng??i dùng ??ng nh?p m?t l?n trên m?t ?ng d?ng trung tâm
- T?t c? ?ng d?ng khác t? ??ng xác th?c ng??i dùng

### Cách Ho?t ??ng Hi?n T?i
```
???????????????????????
?  Web Khác (SSO)     ?  ? X? lý ??ng nh?p
???????????????????????
           ? (Token ho?c Cookie)
           ?
???????????????????????
?  QlyKhachHang.Com   ?  ? Nh?n token, c?p quy?n
???????????????????????
```

### Cách S?a ??i Program.cs
```csharp
// Thay vì Session, s? d?ng Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Ho?c s? d?ng Bearer Token (cho API)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-sso-server.com";
        options.Audience = "your-app-id";
    });
```

---

## 2?? OAuth 2.0 Integration

### Khái Ni?m
- ??ng nh?p qua bên th? ba (Google, Facebook, GitHub, v.v.)

### Cách Th?c Hi?n
```csharp
// Trong Program.cs
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = Configuration["Google:ClientId"];
        options.ClientSecret = Configuration["Google:ClientSecret"];
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = Configuration["MicrosoftAccount:ClientId"];
        options.ClientSecret = Configuration["MicrosoftAccount:ClientSecret"];
    });
```

### S?a ??i Login View
```html
<!-- Views/Account/Login.cshtml -->
<a href="https://accounts.google.com/o/oauth2/v2/auth?client_id=...">
    ??ng Nh?p B?ng Google
</a>
```

---

## 3?? API Integration (G?i API t? ?ng d?ng khác)

### Khái Ni?m
- ?ng d?ng này g?i API c?a ?ng d?ng web khác ?? xác th?c ng??i dùng

### Cách Ho?t ??ng
```
????????????????????????????
?  QlyKhachHang.Com        ?
????????????????????????????
?  POST /Account/Login     ?
?  ?                       ?
?  G?i API:               ?
?  POST /api/auth/login    ?  ? ?ng d?ng SSO
?  ?                       ?
?  Nh?n Token              ?
?  ?                       ?
?  L?u Session             ?
????????????????????????????
```

### S?a ??i AuthenticationService.cs
```csharp
public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    
    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Customer?> LoginAsync(string usernameOrEmail, string password)
    {
        try
        {
            // G?i API t? ?ng d?ng SSO
            var response = await _httpClient.PostAsJsonAsync(
                "https://your-sso-app.com/api/auth/login",
                new { username = usernameOrEmail, password = password }
            );
            
            if (!response.IsSuccessStatusCode)
                return null;
            
            var result = await response.Content.ReadAsAsync<dynamic>();
            
            // L?y ho?c t?o customer t? database hi?n t?i
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Username == usernameOrEmail);
            
            if (customer == null)
            {
                // T?o customer m?i n?u ch?a t?n t?i
                customer = new Customer
                {
                    Username = result.username,
                    Email = result.email,
                    CustomerName = result.name,
                    PasswordHash = "SSO_USER", // ?ánh d?u là SSO
                    Status = "Active"
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling SSO API");
            return null;
        }
    }
}
```

---

## 4?? JWT Token Integration

### Khái Ni?m
- ?ng d?ng khác c?p JWT Token
- ?ng d?ng này xác th?c token và c?p quy?n truy c?p

### Cách S?a ??i Program.cs
```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = Configuration["Jwt:Audience"]
        };
    });
```

### S?a ??i appsettings.json
```json
{
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "https://your-sso-server.com",
    "Audience": "your-app-id",
    "ExpirationMinutes": 30
  }
}
```

### S?a ??i AccountController.cs
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(LoginViewModel model)
{
    // G?i API l?y JWT Token
    var token = await GetJwtTokenFromExternalServer(model.UsernameOrEmail, model.Password);
    
    if (string.IsNullOrEmpty(token))
    {
        ModelState.AddModelError("", "??ng nh?p th?t b?i");
        return View(model);
    }
    
    // L?u token vào Cookie
    Response.Cookies.Append("JwtToken", token, new CookieOptions
    {
        HttpOnly = true,
        Expires = DateTimeOffset.UtcNow.AddMinutes(30)
    });
    
    return RedirectToAction("Index", "Home");
}
```

---

## 5?? Azure AD / Entra ID Integration

### Khái Ni?m
- ??ng nh?p qua Azure Active Directory (dành cho doanh nghi?p)

### Cách S?a ??i Program.cs
```csharp
builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
```

### S?a ??i appsettings.json
```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "your-domain.onmicrosoft.com",
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "CallbackPath": "/signin-oidc"
  }
}
```

---

## 6?? Hybrid Approach (K?t H?p Nhi?u Ph??ng Th?c)

### Khái Ni?m
- H? tr? c? ??ng nh?p c?c b? và SSO

### Ví D?
```csharp
public async Task<IActionResult> Login(LoginViewModel model, string provider = null)
{
    if (!string.IsNullOrEmpty(provider))
    {
        // ??ng nh?p qua SSO/OAuth
        return await LoginViaSso(provider, model);
    }
    else
    {
        // ??ng nh?p c?c b? (nh? hi?n t?i)
        return await LoginLocally(model);
    }
}
```

---

## B?ng So Sánh

| Ph??ng Pháp | ?u ?i?m | Nh??c ?i?m | Phù H?p Khi |
|---|---|---|---|
| **Session** (Hi?n t?i) | ??n gi?n, nhanh | Khó m? r?ng | 1 ?ng d?ng |
| **SSO + Cookie** | Qu?n lý t?p trung | C?u hình ph?c t?p | Nhi?u ?ng d?ng n?i b? |
| **OAuth/OIDC** | An toàn, tiêu chu?n | Ph? thu?c bên th? ba | ??ng nh?p xã h?i |
| **JWT Token** | Stateless, scalable | X? lý ph?c t?p | Microservices, API |
| **Azure AD** | Doanh nghi?p, b?o m?t cao | ??t ti?n | T? ch?c l?n |

---

## Khuy?n Ngh?

D?a trên ki?n trúc hi?n t?i, n?u b?n mu?n tích h?p v?i ?ng d?ng web khác:

### 1. **N?u ch? có m?t ?ng d?ng SSO:**
   - S? d?ng **API Integration** (ph??ng pháp 3)
   - G?i API SSO ?? xác th?c
   - L?u thông tin user vào database hi?n t?i

### 2. **N?u có nhi?u ?ng d?ng c?n ??ng nh?p chung:**
   - S? d?ng **JWT Token** (ph??ng pháp 4)
   - T?t c? ?ng d?ng xác th?c token t? máy ch? trung tâm

### 3. **N?u là doanh nghi?p/t? ch?c l?n:**
   - S? d?ng **Azure AD** (ph??ng pháp 5)
   - Tích h?p v?i h? th?ng qu?n lý nhân s? hi?n có

---

## Các File C?n S?a ??i

1. **Program.cs** - C?u hình authentication
2. **AccountController.cs** - X? lý ??ng nh?p
3. **AuthenticationService.cs** - G?i API/Token validation
4. **appsettings.json** - L?u c?u hình SSO
5. **Login.cshtml** - Thêm nút ??ng nh?p b?ng ph??ng pháp khác

---

**Liên H?:** N?u b?n c?n h? tr? th?c hi?n m?t trong nh?ng ph??ng pháp trên, vui lòng cung c?p thêm chi ti?t v? ?ng d?ng SSO c?a b?n.
