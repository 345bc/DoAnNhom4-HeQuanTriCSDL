using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FashionShop.Helper;
using FashionShop.Models;

namespace FashionShop.DAL
{
    public class BrandDAL
    {
        /// <summary>
        /// Lấy danh sách Brand đang hoạt động
        /// Gọi SP: sp_GetActiveBrands
        /// </summary>
        public static List<Brand> GetActiveBrands()
        {
            List<Brand> brands = new List<Brand>();

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetActiveBrands");

            foreach (DataRow row in dt.Rows)
            {
                brands.Add(new Brand
                {
                    Brand_Id = (int)row["Brand_Id"],
                    Name = row["Name"].ToString()
                });
            }

            return brands;
        }

        /// <summary>
        /// Lấy danh sách Brand với phân trang
        /// </summary>
        public static List<Brand> GetBrandsPaged(int pageNumber, int pageSize, string keyword = "")
        {
            List<Brand> brands = new List<Brand>();
            int offset = (pageNumber - 1) * pageSize;

            string query = @"
                SELECT Brand_Id, Name, Phone, Address, CountryOfOrigin, Email
                FROM Brand
                WHERE (@Keyword IS NULL OR Name LIKE @KeywordPattern OR Description LIKE @KeywordPattern)
                ORDER BY Name
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", string.IsNullOrEmpty(keyword) ? (object)DBNull.Value : keyword),
                new SqlParameter("@KeywordPattern", string.IsNullOrEmpty(keyword) ? "" : "%" + keyword + "%"),
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in dt.Rows)
            {
                brands.Add(new Brand
                {
                    Brand_Id = (int)row["Brand_Id"],
                    Name = row["Name"].ToString(),
                    Phone = row["Phone"]?.ToString() ?? "",
                    Address = row["Address"].ToString(),
                    CountryOfOrigin = row["CountryOfOrigin"]?.ToString() ?? "",
                    Email = row["Email"].ToString()
                });
            }

            return brands;
        }

        /// <summary>
        /// Lấy chi tiết Brand theo ID
        /// </summary>
        public static Brand GetBrandById(int brandId)
        {
            string query = "SELECT Brand_Id, Name, Phone, Address, CountryOfOrigin, Email FROM Brand WHERE Brand_Id = @BrandId";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@BrandId", brandId) };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count == 0)
                return null;

            DataRow row = dt.Rows[0];
            return new Brand
            {
                Brand_Id = (int)row["Brand_Id"],
                Name = row["Name"].ToString(),
                Phone = row["Phone"]?.ToString() ?? "",
                Address = row["Address"].ToString(),
                CountryOfOrigin = row["CountryOfOrigin"]?.ToString() ?? "",
                Email = row["Email"].ToString()
            };
        }

        /// <summary>
        /// Thêm Brand mới
        /// </summary>
        public static OperationResult AddBrand(Brand model)
        {
            string query = @"
                INSERT INTO Brand (Brand_Id, Name, Phone, Address, CountryOfOrigin, Email)
                VALUES (@BrandId, @Name, @Phone, @Address, @CountryOfOrigin, @Email)
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BrandId", model.Brand_Id),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Phone", model.Phone ?? ""),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("@CountryOfOrigin", model.CountryOfOrigin ?? ""),
                new SqlParameter("@Email", model.Email)
            };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return new OperationResult { Success = result > 0, Message = "Thêm Brand thành công!" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }

        /// <summary>
        /// Cập nhật Brand
        /// </summary>
        public static OperationResult UpdateBrand(Brand model)
        {
            string query = @"
                UPDATE Brand
                SET Name = @Name, Phone = @Phone, Address = @Address, CountryOfOrigin = @CountryOfOrigin, Email = @Email
                WHERE Brand_Id = @BrandId
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@BrandId", model.Brand_Id),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Phone", model.Phone ?? ""),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("@CountryOfOrigin", model.CountryOfOrigin ?? ""),
                new SqlParameter("@Email", model.Email)
            };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return new OperationResult { Success = result > 0, Message = "Cập nhật Brand thành công!" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }

        /// <summary>
        /// Xóa Brand
        /// </summary>
        public static OperationResult DeleteBrand(int brandId)
        {
            string query = "DELETE FROM Brand WHERE Brand_Id = @BrandId";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@BrandId", brandId) };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return new OperationResult { Success = result > 0, Message = "Xóa Brand thành công!" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Lỗi: " + ex.Message };
            }
        }
    }
}