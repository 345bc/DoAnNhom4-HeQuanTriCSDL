using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FashionShop.Helper;
using FashionShop.Models;
using FashionShop.Models.ViewModels;

namespace FashionShop.DAL
{
    public class ProductDAL
    {
        public static List<ProductViewModel> GetProductsPaged(int pageNumber, int pageSize,
            string keyword = "", int? categoryId = null, int? brandId = null)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", string.IsNullOrEmpty(keyword) ? (object)DBNull.Value : keyword),
                new SqlParameter("@Category_Id", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value),
                new SqlParameter("@Brand_Id", brandId.HasValue ? (object)brandId.Value : DBNull.Value),
                new SqlParameter("@PageNumber", pageNumber),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@SortBy", "newest")
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_SearchProducts", parameters);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(new ProductViewModel
                {
                    Product_Id = (int)row["Product_Id"],
                    ProductName = row["ProductName"].ToString(),
                    ProductSlug = row["ProductSlug"].ToString(),
                    Price = (decimal)row["Price"],
                    SalePrice = row["SalePrice"] != DBNull.Value ? (decimal?)row["SalePrice"] : null,
                    Quantity = (int)row["Quantity"],
                    Category_Id = (int)row["Category_Id"],
                    CategoryName = row["CategoryName"].ToString(),
                    Brand_Id = row["BrandName"] != DBNull.Value ? 1 : 0,
                    BrandName = row["BrandName"]?.ToString() ?? ""
                });
            }

            return products;
        }

        /// <summary>
        /// Lấy chi tiết sản phẩm theo ID
        /// Gọi SP: sp_GetProductDetail
        /// </summary>
        public static ProductViewModel GetProductById(int productId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Product_Id", productId)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetProductDetail", parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new ProductViewModel
            {
                Product_Id = (int)row["Product_Id"],
                ProductName = row["ProductName"].ToString(),
                ShortDescription = row["ShortDescription"]?.ToString() ?? "",
                FullDescription = row["FullDescription"]?.ToString() ?? "",
                Price = (decimal)row["Price"],
                SalePrice = row["SalePrice"] != DBNull.Value ? (decimal?)row["SalePrice"] : null,
                Quantity = (int)row["Quantity"],
                Category_Id = (int)row["Category_Id"],
                CategoryName = row["CategoryName"].ToString(),
                Brand_Id = (int)row["Brand_Id"],
                BrandName = row["BrandName"].ToString()
            };
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// Gọi SP: sp_AddOrUpdateProduct
        /// </summary>
        public static OperationResult AddProduct(ProductViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Product_Id", 0),
                new SqlParameter("@ProductName", model.ProductName),
                new SqlParameter("@ProductSlug", model.ProductSlug ?? ""),
                new SqlParameter("@ShortDescription", model.ShortDescription ?? (object)DBNull.Value),
                new SqlParameter("@FullDescription", model.FullDescription ?? (object)DBNull.Value),
                new SqlParameter("@Price", model.Price),
                new SqlParameter("@SalePrice", model.SalePrice.HasValue ? (object)model.SalePrice.Value : DBNull.Value),
                new SqlParameter("@Quantity", model.Quantity),
                new SqlParameter("@Category_Id", model.Category_Id),
                new SqlParameter("@Brand_Id", model.Brand_Id),
                new SqlParameter("@ActionBy", System.Web.HttpContext.Current?.User?.Identity?.Name ?? "System")
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_AddOrUpdateProduct", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();
                return new OperationResult { Success = status == "SUCCESS", Message = message };
            }

            return new OperationResult { Success = false, Message = "Không thể thêm sản phẩm" };
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// Gọi SP: sp_AddOrUpdateProduct
        /// </summary>
        public static OperationResult UpdateProduct(ProductViewModel model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Product_Id", model.Product_Id),
                new SqlParameter("@ProductName", model.ProductName),
                new SqlParameter("@ProductSlug", model.ProductSlug ?? ""),
                new SqlParameter("@ShortDescription", model.ShortDescription ?? (object)DBNull.Value),
                new SqlParameter("@FullDescription", model.FullDescription ?? (object)DBNull.Value),
                new SqlParameter("@Price", model.Price),
                new SqlParameter("@SalePrice", model.SalePrice.HasValue ? (object)model.SalePrice.Value : DBNull.Value),
                new SqlParameter("@Quantity", model.Quantity),
                new SqlParameter("@Category_Id", model.Category_Id),
                new SqlParameter("@Brand_Id", model.Brand_Id),
                new SqlParameter("@ActionBy", System.Web.HttpContext.Current?.User?.Identity?.Name ?? "System")
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_AddOrUpdateProduct", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();
                return new OperationResult { Success = status == "SUCCESS", Message = message };
            }

            return new OperationResult { Success = false, Message = "Không thể cập nhật sản phẩm" };
        }

        /// <summary>
        /// Xóa sản phẩm
        /// Gọi SP: sp_DeleteProduct
        /// </summary>
        public static OperationResult DeleteProduct(int productId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Product_Id", productId),
                new SqlParameter("@DeletedBy", System.Web.HttpContext.Current?.User?.Identity?.Name ?? "System"),
                new SqlParameter("@HardDelete", 0) // Soft delete
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_DeleteProduct", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();
                return new OperationResult { Success = status == "SUCCESS", Message = message };
            }

            return new OperationResult { Success = false, Message = "Không thể xóa sản phẩm" };
        }

        /// <summary>
        /// Lấy tổng số sản phẩm
        /// </summary>
        public static int GetTotalProductCount()
        {
            string query = "SELECT COUNT(*) FROM Product WHERE IsActive = 1";
            object result = DatabaseHelper.ExecuteScalar(query);
            return result != null ? (int)result : 0;
        }
    }
}