# Empty State & Data Display Standards

## ?? Overview

This document provides standardized patterns for handling empty states across all table views in the QlyKhachHang application. When a table has no data, it should display a user-friendly message. When data exists, it should automatically be displayed and pushed up on the page.

---

## ?? Pattern Overview

### Simple Pattern (For basic tables)
```razor
@if (Model.Any())
{
    <!-- TABLE HERE -->
}
else
{
    <div class="alert alert-info">
        <p>Không có [Entity Name] nào. <a asp-action="Create">Thêm [Entity Name] m?i</a></p>
    </div>
}
```

### Advanced Pattern (For grids with filters)
```razor
@if (Model.Any())
{
    <!-- FILTER CARD (if applicable) -->
    <!-- TABLE HERE -->
}
else
{
    <div class="alert alert-info alert-lg text-center" role="alert">
        <div class="empty-state">
            <i class="fas fa-[ICON]"></i>
            <h4>[Empty Title]</h4>
            <p class="text-muted">[Empty Description]</p>
            <a asp-action="Create" class="btn btn-primary mt-3">
                <i class="fas fa-plus"></i> Thêm [Entity Name] m?i
            </a>
        </div>
    </div>
}
```

---

## ?? Detailed Implementation Guide

### 1. Customer Table (? DONE)

**File**: `Views/Customer/Index.cshtml`

**Features**:
- Advanced empty state with icon
- Comprehensive search/filter controls
- Grid-based layout
- Pagination support

**Status**: ? Fully Implemented with Advanced Pattern

---

### 2. Product Table (? DONE)

**File**: `Views/Product/Index.cshtml`

**Current Implementation**:
```razor
@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <!-- Table content -->
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có s?n ph?m nào. <a asp-action="Create">Thêm s?n ph?m m?i</a></p>
    </div>
}
```

**Status**: ? Implemented with Simple Pattern

---

### 3. Cart Table (? DONE)

**File**: `Views/Cart/Index.cshtml`

**Current Implementation**:
```razor
@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <!-- Table content -->
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có gi? hàng nào. <a asp-action="Create">T?o gi? hàng m?i</a></p>
    </div>
}
```

**Status**: ? Implemented with Simple Pattern

---

### 4. Review Table (?? NEEDS IMPLEMENTATION)

**File**: `Views/Review/Index.cshtml`

**Recommended Implementation**:
```razor
@model IEnumerable<QlyKhachHang.Models.Review>

@{
    ViewData["Title"] = "Danh Sách ?ánh Giá";
}

<div class="row mb-3">
    <div class="col-md-6">
        <h2>@ViewData["Title"]</h2>
    </div>
    <div class="col-md-6 text-end">
        <a asp-action="Create" class="btn btn-primary">
            <i class="fas fa-plus"></i> Thêm ?ánh Giá
        </a>
    </div>
</div>

@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Khách Hàng</th>
                    <th>S?n Ph?m</th>
                    <th>?ánh Giá</th>
                    <th>Bình Lu?n</th>
                    <th>Hành ??ng</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model)
                {
                    <tr>
                        <td>@item.ReviewId</td>
                        <td>@item.Customer?.CustomerName</td>
                        <td>@item.Product?.ProductName</td>
                        <td>
                            <span class="badge bg-warning">
                                @for (int i = 0; i < item.Rating; i++)
                                {
                                    <i class="fas fa-star"></i>
                                }
                            </span>
                        </td>
                        <td>@item.Comment</td>
                        <td>
                            <a asp-action="Details" asp-route-id="@item.ReviewId" class="btn btn-info btn-sm btn-action">
                                <i class="fas fa-eye"></i> Xem
                            </a>
                            <a asp-action="Edit" asp-route-id="@item.ReviewId" class="btn btn-warning btn-sm btn-action">
                                <i class="fas fa-edit"></i> S?a
                            </a>
                            <a asp-action="Delete" asp-route-id="@item.ReviewId" class="btn btn-danger btn-sm btn-action">
                                <i class="fas fa-trash"></i> Xóa
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có ?ánh giá nào. <a asp-action="Create">Thêm ?ánh giá m?i</a></p>
    </div>
}
```

**Status**: ?? Template Provided

---

### 5. Invoice Table (?? NEEDS IMPLEMENTATION)

**File**: `Views/Invoice/Index.cshtml`

**Recommended Implementation**:
```razor
@model IEnumerable<QlyKhachHang.Models.Invoice>

@{
    ViewData["Title"] = "Danh Sách Hóa ??n";
}

<div class="row mb-3">
    <div class="col-md-6">
        <h2>@ViewData["Title"]</h2>
    </div>
    <div class="col-md-6 text-end">
        <a asp-action="Create" class="btn btn-primary">
            <i class="fas fa-plus"></i> T?o Hóa ??n
        </a>
    </div>
</div>

@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>S? Hóa ??n</th>
                    <th>Khách Hàng</th>
                    <th>Ngày T?o</th>
                    <th>T?ng Ti?n</th>
                    <th>Tr?ng Thái</th>
                    <th>Hành ??ng</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model)
                {
                    <tr>
                        <td>@item.InvoiceId</td>
                        <td>@item.InvoiceNumber</td>
                        <td>@item.Customer?.CustomerName</td>
                        <td>@item.InvoiceDate.ToString("dd/MM/yyyy")</td>
                        <td>@item.TotalAmount.ToString("C0")</td>
                        <td>
                            @{
                                string statusClass = item.Status switch
                                {
                                    "Pending" => "bg-warning",
                                    "Completed" => "bg-success",
                                    "Cancelled" => "bg-danger",
                                    _ => "bg-secondary"
                                };
                                string statusText = item.Status switch
                                {
                                    "Pending" => "Ch? x? lý",
                                    "Completed" => "Hoàn thành",
                                    "Cancelled" => "H?y",
                                    _ => "Khác"
                                };
                            }
                            <span class="badge @statusClass">@statusText</span>
                        </td>
                        <td>
                            <a asp-action="Details" asp-route-id="@item.InvoiceId" class="btn btn-info btn-sm btn-action">
                                <i class="fas fa-eye"></i> Xem
                            </a>
                            <a asp-action="Edit" asp-route-id="@item.InvoiceId" class="btn btn-warning btn-sm btn-action">
                                <i class="fas fa-edit"></i> S?a
                            </a>
                            <a asp-action="Delete" asp-route-id="@item.InvoiceId" class="btn btn-danger btn-sm btn-action">
                                <i class="fas fa-trash"></i> Xóa
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có hóa ??n nào. <a asp-action="Create">T?o hóa ??n m?i</a></p>
    </div>
}
```

**Status**: ?? Template Provided

---

### 6. InvoiceDetail Table (?? NEEDS IMPLEMENTATION)

**File**: `Views/InvoiceDetail/Index.cshtml`

**Recommended Implementation**:
```razor
@model IEnumerable<QlyKhachHang.Models.InvoiceDetail>

@{
    ViewData["Title"] = "Danh Sách Chi Ti?t Hóa ??n";
}

<div class="row mb-3">
    <div class="col-md-6">
        <h2>@ViewData["Title"]</h2>
    </div>
    <div class="col-md-6 text-end">
        <a asp-action="Create" class="btn btn-primary">
            <i class="fas fa-plus"></i> Thêm Chi Ti?t
        </a>
    </div>
</div>

@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Hóa ??n</th>
                    <th>S?n Ph?m</th>
                    <th>S? L??ng</th>
                    <th>??n Giá</th>
                    <th>Thành Ti?n</th>
                    <th>Hành ??ng</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model)
                {
                    <tr>
                        <td>@item.InvoiceDetailId</td>
                        <td>@item.Invoice?.InvoiceNumber</td>
                        <td>@item.Product?.ProductName</td>
                        <td>@item.Quantity</td>
                        <td>@item.UnitPrice.ToString("C0")</td>
                        <td>@((item.Quantity * item.UnitPrice).ToString("C0"))</td>
                        <td>
                            <a asp-action="Details" asp-route-id="@item.InvoiceDetailId" class="btn btn-info btn-sm btn-action">
                                <i class="fas fa-eye"></i> Xem
                            </a>
                            <a asp-action="Edit" asp-route-id="@item.InvoiceDetailId" class="btn btn-warning btn-sm btn-action">
                                <i class="fas fa-edit"></i> S?a
                            </a>
                            <a asp-action="Delete" asp-route-id="@item.InvoiceDetailId" class="btn btn-danger btn-sm btn-action">
                                <i class="fas fa-trash"></i> Xóa
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có chi ti?t hóa ??n nào. <a asp-action="Create">Thêm chi ti?t m?i</a></p>
    </div>
}
```

**Status**: ?? Template Provided

---

### 7. CartItem Table (?? NEEDS IMPLEMENTATION)

**File**: `Views/CartItem/Index.cshtml`

**Recommended Implementation**:
```razor
@model IEnumerable<QlyKhachHang.Models.CartItem>

@{
    ViewData["Title"] = "Danh Sách Chi Ti?t Gi? Hàng";
}

<div class="row mb-3">
    <div class="col-md-6">
        <h2>@ViewData["Title"]</h2>
    </div>
    <div class="col-md-6 text-end">
        <a asp-action="Create" class="btn btn-primary">
            <i class="fas fa-plus"></i> Thêm S?n Ph?m
        </a>
    </div>
</div>

@if (Model.Any())
{
    <div class="table-responsive">
        <table class="table table-hover table-striped">
            <thead class="table-dark">
                <tr>
                    <th>ID</th>
                    <th>Gi? Hàng</th>
                    <th>S?n Ph?m</th>
                    <th>S? L??ng</th>
                    <th>??n Giá</th>
                    <th>T?ng C?ng</th>
                    <th>Ngày Thêm</th>
                    <th>Hành ??ng</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in Model)
                {
                    <tr>
                        <td>@item.CartItemId</td>
                        <td>Gi? #@item.CartId</td>
                        <td>@item.Product?.ProductName</td>
                        <td>@item.Quantity</td>
                        <td>@item.UnitPrice.ToString("C0")</td>
                        <td>@((item.Quantity * item.UnitPrice).ToString("C0"))</td>
                        <td>@item.AddedDate.ToString("dd/MM/yyyy HH:mm")</td>
                        <td>
                            <a asp-action="Details" asp-route-id="@item.CartItemId" class="btn btn-info btn-sm btn-action">
                                <i class="fas fa-eye"></i> Xem
                            </a>
                            <a asp-action="Edit" asp-route-id="@item.CartItemId" class="btn btn-warning btn-sm btn-action">
                                <i class="fas fa-edit"></i> S?a
                            </a>
                            <a asp-action="Delete" asp-route-id="@item.CartItemId" class="btn btn-danger btn-sm btn-action">
                                <i class="fas fa-trash"></i> Xóa
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có s?n ph?m nào trong gi? hàng. <a asp-action="Create">Thêm s?n ph?m m?i</a></p>
    </div>
}
```

**Status**: ?? Template Provided

---

## ?? Empty State CSS Classes

All empty states use these CSS classes (defined in `fashion-shop.css`):

```css
.alert-info {
    background-color: #d1ecf1;
    border-left-color: #17a2b8;
    color: #0c5460;
}

.empty-state {
    padding: 60px 20px;
    text-align: center;
}

.empty-state i {
    font-size: 4rem;
    color: #17a2b8;
    margin-bottom: 20px;
    opacity: 0.3;
}

.empty-state h4 {
    font-weight: 600;
    color: #333;
    margin-bottom: 10px;
}
```

---

## ? Implementation Checklist

### ? Completed
- [x] Customer Table - Advanced pattern with grid
- [x] Product Table - Simple pattern
- [x] Cart Table - Simple pattern

### ? To Implement
- [ ] Review Table - Simple pattern
- [ ] Invoice Table - Simple pattern
- [ ] InvoiceDetail Table - Simple pattern
- [ ] CartItem Table - Simple pattern

---

## ?? Responsive Behavior

### On Desktop (1200px+)
- Full table with all columns visible
- Alert centered with icon and text
- Button spans full width in empty state

### On Tablet (768px - 1199px)
- Table may overflow horizontally
- Scrollbar appears for wide tables
- Alert scales appropriately

### On Mobile (< 768px)
- Table becomes scrollable if needed
- Stack layout for better readability
- Buttons remain accessible with proper padding

---

## ?? Best Practices

### 1. Always Use Model.Any()
```razor
@if (Model.Any())  ? CORRECT
@if (Model != null && Model.Count() > 0)  ? INEFFICIENT
```

### 2. Consistent Empty State Messages
```
Không có [Entity Name] nào. [Create Link]
```

### 3. Provide Action Link
Always include a link to create new item when empty:
```razor
<a asp-action="Create">Thêm [Entity Name] m?i</a>
```

### 4. Use Alert for Visual Feedback
- ?? Use `alert-info` for informational messages
- ? Use `alert-success` for success notifications
- ?? Use `alert-warning` for warnings
- ? Use `alert-danger` for errors

---

## ?? Advanced Patterns

### Pattern 1: Empty State with Search
```razor
@if (Model.Any())
{
    <!-- Table -->
}
else if (!string.IsNullOrEmpty(searchTerm))
{
    <div class="alert alert-info">
        <p>Không tìm th?y k?t qu? cho "[searchTerm]"</p>
    </div>
}
else
{
    <div class="alert alert-info">
        <p>Không có d? li?u nào. <a asp-action="Create">T?o m?i</a></p>
    </div>
}
```

### Pattern 2: Empty State with Icons
```razor
<div class="alert alert-info alert-lg">
    <div class="empty-state">
        <i class="fas fa-inbox"></i>
        <h4>Không có d? li?u</h4>
        <p class="text-muted">Ch?a có b?n ghi nào...</p>
        <a asp-action="Create" class="btn btn-primary mt-3">
            <i class="fas fa-plus"></i> T?o m?i
        </a>
    </div>
</div>
```

---

## ?? Customization Guide

### Change Empty State Text
Edit your view's empty state block with appropriate Vietnamese text.

### Change Empty State Icon
Replace `fa-inbox` with any Font Awesome icon:
- `fa-users` - Users/Customers
- `fa-box` - Products
- `fa-shopping-cart` - Carts
- `fa-star` - Reviews
- `fa-receipt` - Invoices

### Change Empty State Color
Modify alert class:
- `alert-info` - Blue (Informational)
- `alert-warning` - Yellow (Caution)
- `alert-light` - Gray (Neutral)

---

## ?? Summary

| Table | View File | Status | Pattern | Notes |
|-------|-----------|--------|---------|-------|
| Customer | `Views/Customer/Index.cshtml` | ? Done | Advanced | With grid & filters |
| Product | `Views/Product/Index.cshtml` | ? Done | Simple | Basic pattern |
| Cart | `Views/Cart/Index.cshtml` | ? Done | Simple | Basic pattern |
| Review | `Views/Review/Index.cshtml` | ?? Template | Simple | Ready to implement |
| Invoice | `Views/Invoice/Index.cshtml` | ?? Template | Simple | Ready to implement |
| InvoiceDetail | `Views/InvoiceDetail/Index.cshtml` | ?? Template | Simple | Ready to implement |
| CartItem | `Views/CartItem/Index.cshtml` | ?? Template | Simple | Ready to implement |

---

**Note**: All "Hi?n không có d? li?u nào" (Currently there is no data) messages will automatically not display because of the `@if (Model.Any())` check. The data will automatically be pushed up and displayed when available.
