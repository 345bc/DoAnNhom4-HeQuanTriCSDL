# Customer Management Grid System - Implementation Guide

## Overview
A professional grid-based customer management interface has been implemented with advanced features including search, filtering, pagination, and sorting capabilities.

## Features Implemented

### 1. Backend Enhancement (CustomerController.cs)

#### Search Functionality
- Search across multiple fields: CustomerName, Phone, Email, City
- Real-time filtering as user types

#### Filtering
- Filter by customer status: Active, Inactive, Blocked
- Supports multiple simultaneous filters

#### Sorting
- Sort by: Customer Name, Creation Date, Email, Customer ID
- Ascending (A ? Z) or Descending (Z ? A) order
- Visual indicators (? ?) showing current sort direction

#### Pagination
- 10 customers per page (configurable via PAGE_SIZE)
- Smart pagination with range display (e.g., "Trang 1 / 5")
- Previous/Next navigation with ellipsis (...) for large ranges
- Jump to first/last page buttons

### 2. Frontend Enhancement (Views/Customer/Index.cshtml)

#### Grid Interface Features
- **Header Section**: Page title with total customer count and add new button
- **Filter Panel**: Advanced search and filter controls
- **Data Table**: Professional grid display with hover effects
- **Status Indicators**: Color-coded badges (Green=Active, Yellow=Inactive, Red=Blocked)
- **Action Buttons**: View, Edit, Delete operations with icons
- **Responsive Design**: Mobile-friendly layout

#### Interactive Elements
- **Checkbox Selection**: Select/deselect all rows with master checkbox
- **Sorting Links**: Click column headers to change sort order
- **Search Bar**: Real-time filtering input with icon
- **Filter Dropdowns**: Status and sort order selection
- **Pagination Controls**: Smart page navigation

### 3. Styling (wwwroot/css/customer-management.css)

#### Visual Design
- Gradient header with purple/blue theme
- Hover effects on table rows (light background with left border highlight)
- Professional card-based layout with shadows
- Color-coded status badges
- Responsive media queries for mobile devices

#### Key CSS Classes
```css
.customer-management-container    /* Main wrapper */
.management-header               /* Page header section */
.page-title                       /* Large title with icon */
.filter-card                      /* Filter section styling */
.customer-grid                    /* Data table styling */
.table-card                       /* Table container */
.empty-state                      /* No data message */
```

## Database Interaction

### Customer Model Relationships
- **Carts**: One customer ? Many carts (Cascade delete on cart only)
- **Reviews**: One customer ? Many reviews
- **Invoices**: One customer ? Many invoices

### Data Validation
- Email uniqueness constraint
- Username uniqueness constraint
- Required fields: CustomerName, Phone, Email, Username, PasswordHash

## API Endpoints

### GET /Customer/Index
**Parameters:**
- `searchTerm` (string): Search text for name, phone, email, city
- `sortBy` (string): Column to sort by (CustomerName, CreatedDate, Email, CustomerId)
- `sortOrder` (string): asc or desc
- `statusFilter` (string): Active, Inactive, or Blocked
- `page` (int): Page number (default: 1)

**Response:** 
- List of customers (10 per page)
- ViewData contains: SearchTerm, SortBy, SortOrder, StatusFilter, CurrentPage, TotalPages, TotalCount

### POST /Customer/Create
**Parameters:**
- CustomerName, Phone, Email, Address, City, PostalCode, Username, Status

### POST /Customer/Edit/{id}
**Parameters:**
- CustomerId, CustomerName, Phone, Email, Address, City, PostalCode, Username, Status

### POST /Customer/Delete/{id}
**Validation:**
- Checks for related Invoices, Reviews, and Carts
- Prevents deletion if relationships exist

## Code Quality Features

### Error Handling
- Try-catch blocks in all async operations
- Logging of exceptions with ILogger
- User-friendly error messages via TempData

### Performance
- Async/await for database operations
- Single database query with filtering applied at database level
- Pagination to limit result set size

### Security
- CSRF token validation on POST operations
- Status field values restricted to enum (Active, Inactive, Blocked)
- Input validation via model binding

## Browser Compatibility

? Chrome/Chromium (latest)
? Firefox (latest)
? Safari (latest)
? Edge (latest)
? Mobile browsers (iOS Safari, Chrome Mobile)

## File Structure

```
QlyKhachHang/
??? Controllers/
?   ??? CustomerController.cs              (Enhanced with search, filter, pagination)
??? Views/
?   ??? Customer/
?   ?   ??? Index.cshtml                   (New grid-based interface)
?   ??? Shared/
?       ??? _Layout.cshtml                 (Updated with CSS reference)
??? wwwroot/
    ??? css/
        ??? customer-management.css        (New styling file)
```

## Customization Guide

### Change Items Per Page
Edit `CustomerController.cs`:
```csharp
private const int PAGE_SIZE = 10;  // Change to desired number
```

### Change Default Sort
Edit `CustomerController.cs` in Index method:
```csharp
sortBy = "CustomerName"  // Change to: CreatedDate, Email, CustomerId
```

### Modify Colors
Edit `wwwroot/css/customer-management.css`:
```css
.customer-grid thead th {
    background: linear-gradient(135deg, #YOUR_COLOR_1 0%, #YOUR_COLOR_2 100%);
}
```

### Add Additional Search Fields
Edit `CustomerController.cs` Index method:
```csharp
query = query.Where(c => 
    c.CustomerName.Contains(searchTerm) ||
    c.Phone.Contains(searchTerm) ||
    c.Email.Contains(searchTerm) ||
    c.City.Contains(searchTerm) ||
    c.PostalCode.Contains(searchTerm)  // Add this line
);
```

## Testing Recommendations

1. **Functionality Testing**
   - Test search with partial and full terms
   - Verify pagination with different page sizes
   - Test sorting in both ascending and descending order
   - Verify filtering by each status value

2. **Responsive Testing**
   - Test on desktop (1920px, 1366px, 1024px)
   - Test on tablet (768px, 810px)
   - Test on mobile (375px, 414px)

3. **Performance Testing**
   - Test with 1000+ records
   - Verify pagination load times
   - Check search query performance

4. **Error Handling**
   - Test delete operation with related records
   - Test invalid page numbers
   - Test special characters in search

## Future Enhancements

- [ ] Export to Excel/PDF functionality
- [ ] Bulk operations (edit, delete multiple)
- [ ] Advanced filters with date ranges
- [ ] Customer activity timeline
- [ ] Custom column selection
- [ ] Save filter preferences
- [ ] Real-time search with debouncing
- [ ] Column resizing and reordering
- [ ] Email notifications for customer actions

## Support

For issues or questions about the customer management system:
1. Check browser console for JavaScript errors
2. Review application logs for backend errors
3. Verify database connectivity
4. Clear browser cache if styling issues occur
