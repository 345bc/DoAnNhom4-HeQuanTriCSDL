//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using QlyDonHang_DoAn_hqtcsdl.Models;

//namespace QlyDonHang_DoAn_hqtcsdl.Helper
//{
//    public static class CartHelper
//    {
//        /// <summary>
//        /// Lấy MaKH (ID khách hàng) từ Session
//        /// </summary>
//        /// <returns>MaKH hoặc null nếu chưa đăng nhập</returns>
//        public static int? GetCurrentCustomerId()
//        {
//            if (HttpContext.Current.Session["MaKH"] != null)
//            {
//                return Convert.ToInt32(HttpContext.Current.Session["MaKH"]);
//            }
//            return null;
//        }

//        /// <summary>
//        /// Kiểm tra người dùng đã đăng nhập chưa
//        /// </summary>
//        /// <returns>True nếu đã đăng nhập, False nếu chưa</returns>
//        public static bool IsLoggedIn()
//        {
//            return HttpContext.Current.Session["MaKH"] != null;
//        }

//        /// <summary>
//        /// Đếm tổng số sản phẩm trong giỏ hàng (tính theo số lượng)
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <returns>Tổng số sản phẩm</returns>
//        public static int GetCartItemCount(FashionShopDb db)
//        {
//            var maKH = GetCurrentCustomerId();
//            if (maKH == null) return 0;

//            try
//            {
//                var count = db.Carts
//                    .Where(c => c.MaKH == maKH.Value)
//                    .Sum(c => (int?)c.Quantity) ?? 0;

//                // Lưu vào Session để sử dụng ở View (Header)
//                HttpContext.Current.Session["CartCount"] = count;
//                return count;
//            }
//            catch
//            {
//                return 0;
//            }
//        }

//        /// <summary>
//        /// Tính tổng tiền giỏ hàng của khách hàng
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="maKH">Mã khách hàng</param>
//        /// <returns>Tổng tiền (VNĐ)</returns>
//        public static decimal CalculateCartTotal(FashionShopDb db, int maKH)
//        {
//            try
//            {
//                return db.Carts
//                    .Where(c => c.MaKH == maKH)
//                    .Join(db.Products,
//                        cart => cart.Product_Id,
//                        product => product.Product_Id,
//                        (cart, product) => new
//                        {
//                            Quantity = cart.Quantity,
//                            Price = product.SalePrice ?? product.Price
//                        })
//                    .Sum(x => x.Quantity * x.Price);
//            }
//            catch
//            {
//                return 0;
//            }
//        }

//        /// <summary>
//        /// Format số tiền theo định dạng VNĐ
//        /// </summary>
//        /// <param name="amount">Số tiền</param>
//        /// <returns>Chuỗi đã format (VD: 1.000.000 ₫)</returns>
//        public static string FormatCurrency(decimal amount)
//        {
//            return string.Format("{0:N0} ₫", amount);
//        }

//        /// <summary>
//        /// Kiểm tra sản phẩm còn đủ hàng trong kho không
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="productId">ID sản phẩm</param>
//        /// <param name="requestedQuantity">Số lượng yêu cầu</param>
//        /// <returns>True nếu đủ hàng, False nếu không đủ</returns>
//        public static bool CheckStockAvailability(FashionShopDb db, int productId, int requestedQuantity)
//        {
//            try
//            {
//                var product = db.Products.Find(productId);
//                if (product == null) return false;

//                return product.Quantity >= requestedQuantity;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// Tạo mã đơn hàng tự động (Order Number)
//        /// </summary>
//        /// <returns>Mã đơn hàng (VD: ORD20250127143025)</returns>
//        public static string GenerateOrderNumber()
//        {
//            return "ORD" + DateTime.Now.ToString("yyyyMMddHHmmss");
//        }

//        /// <summary>
//        /// Kiểm tra và validate mã giảm giá (Coupon)
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="couponCode">Mã giảm giá</param>
//        /// <param name="maKH">Mã khách hàng</param>
//        /// <param name="subTotal">Tổng tiền trước khi giảm</param>
//        /// <returns>Tuple (IsValid, Message, DiscountAmount)</returns>
//        public static (bool IsValid, string Message, decimal DiscountAmount) ValidateCoupon(
//            FashionShopDb db,
//            string couponCode,
//            int maKH,
//            decimal subTotal)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(couponCode))
//                    return (false, "Vui lòng nhập mã giảm giá", 0);

//                var coupon = db.Coupons.FirstOrDefault(c =>
//                    c.CouponCode == couponCode &&
//                    c.IsActive &&
//                    c.StartDate <= DateTime.Now &&
//                    c.ExpiryDate >= DateTime.Now);

//                if (coupon == null)
//                    return (false, "Mã giảm giá không hợp lệ hoặc đã hết hạn", 0);

//                // Kiểm tra số lần sử dụng của khách hàng
//                if (coupon.UsageLimitPerUser.HasValue)
//                {
//                    var usageCount = db.Coupon_Usage.Count(cu =>
//                        cu.Coupon_Id == coupon.Coupon_Id &&
//                        cu.MaKH == maKH);

//                    if (usageCount >= coupon.UsageLimitPerUser.Value)
//                        return (false, "Bạn đã sử dụng hết lượt giảm giá này", 0);
//                }

//                // Tính số tiền giảm giá
//                decimal discountAmount = 0;

//                if (coupon.DiscountType == 1) // Giảm theo phần trăm (%)
//                {
//                    discountAmount = subTotal * (coupon.DiscountValue / 100);
//                }
//                else if (coupon.DiscountType == 2) // Giảm theo số tiền cố định (VNĐ)
//                {
//                    discountAmount = coupon.DiscountValue;
//                }

//                // Không cho phép giảm giá lớn hơn tổng tiền
//                discountAmount = Math.Min(discountAmount, subTotal);

//                return (true, $"Áp dụng mã giảm {FormatCurrency(discountAmount)} thành công!", discountAmount);
//            }
//            catch (Exception ex)
//            {
//                return (false, "Có lỗi xảy ra: " + ex.Message, 0);
//            }
//        }

//        /// <summary>
//        /// Xóa các Session liên quan đến giỏ hàng
//        /// </summary>
//        public static void ClearCartSession()
//        {
//            HttpContext.Current.Session["CartCount"] = 0;
//        }

//        /// <summary>
//        /// Lấy số lượng giỏ hàng từ Session (nếu có)
//        /// </summary>
//        /// <returns>Số lượng sản phẩm</returns>
//        public static int GetCartCountFromSession()
//        {
//            if (HttpContext.Current.Session["CartCount"] != null)
//            {
//                return Convert.ToInt32(HttpContext.Current.Session["CartCount"]);
//            }
//            return 0;
//        }

//        /// <summary>
//        /// Kiểm tra khách hàng đã mua sản phẩm này chưa (để review)
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="maKH">Mã khách hàng</param>
//        /// <param name="productId">ID sản phẩm</param>
//        /// <returns>True nếu đã mua, False nếu chưa</returns>
//        public static bool HasPurchasedProduct(FashionShopDb db, int maKH, int productId)
//        {
//            try
//            {
//                return db.OrderDetails
//                    .Any(od => od.Order.MaKH == maKH
//                            && od.Product_Id == productId
//                            && od.Order.Status == "Hoàn thành"
//                            && !od.Order.IsCancel);
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// Lấy tổng số đơn hàng của khách hàng
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="maKH">Mã khách hàng</param>
//        /// <returns>Số đơn hàng</returns>
//        public static int GetCustomerOrderCount(FashionShopDb db, int maKH)
//        {
//            try
//            {
//                return db.Orders.Count(o => o.MaKH == maKH && !o.IsCancel);
//            }
//            catch
//            {
//                return 0;
//            }
//        }

//        /// <summary>
//        /// Tính phí ship dựa trên tổng tiền
//        /// </summary>
//        /// <param name="subTotal">Tổng tiền hàng</param>
//        /// <returns>Phí ship (VNĐ)</returns>
//        public static decimal CalculateShippingFee(decimal subTotal)
//        {
//            // Miễn phí ship cho đơn hàng trên 500.000đ
//            if (subTotal >= 500000)
//                return 0;

//            // Phí ship cố định 30.000đ
//            return 30000;
//        }

//        /// <summary>
//        /// Lấy thông tin khách hàng hiện tại
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <returns>Object KhachHang hoặc null</returns>
//        public static KhachHang GetCurrentCustomer(FashionShopDb db)
//        {
//            var maKH = GetCurrentCustomerId();
//            if (maKH == null) return null;

//            try
//            {
//                return db.KhachHangs.Find(maKH.Value);
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Lấy danh sách sản phẩm trong giỏ hàng cho Mini Cart
//        /// </summary>
//        /// <param name="db">Database context</param>
//        /// <param name="maKH">Mã khách hàng</param>
//        /// <returns>Danh sách item trong giỏ</returns>
//        public static System.Collections.Generic.List<dynamic> GetMiniCartItems(FashionShopDb db, int maKH)
//        {
//            try
//            {
//                return db.Carts
//                    .Where(c => c.MaKH == maKH)
//                    .Select(c => new
//                    {
//                        Cart_Id = c.Cart_Id,
//                        Product_Id = c.Product_Id,
//                        ProductName = c.Product.ProductName,
//                        ProductSlug = c.Product.ProductSlug,
//                        ImageUrl = c.Product.Product_Image_Mapping
//                            .Where(m => m.Product_Id == c.Product_Id)
//                            .Select(m => m.ProductImage)
//                            .Where(img => img.IsDefault)
//                            .Select(img => img.ImageUrl)
//                            .FirstOrDefault() ?? "/Content/images/no-image.jpg",
//                        DisplayPrice = c.Product.SalePrice ?? c.Product.Price,
//                        Quantity = c.Quantity,
//                        TotalPrice = (c.Product.SalePrice ?? c.Product.Price) * c.Quantity
//                    })
//                    .ToList<dynamic>();
//            }
//            catch
//            {
//                return new System.Collections.Generic.List<dynamic>();
//            }
//        }
//    }
//}