using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using FashionShop.Helper;

namespace FashionShop.Models
{
    public class PermissionHelper
    {
        /// <summary>
        /// Kiểm tra user có thuộc role nào không
        /// </summary>
        public static bool HasPermission(string userName, string roleName)
        {
            try
            {
                string query = "SELECT 1 FROM Users WHERE Name = @UserName AND Role = @RoleName";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userName ?? ""),
                    new SqlParameter("@RoleName", roleName ?? "")
                };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy role của user
        /// </summary>
        public static string GetUserRole(string userName)
        {
            try
            {
                string query = "SELECT Role FROM Users WHERE Name = @UserName";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@UserName", userName ?? "") };

                object result = DatabaseHelper.ExecuteScalar(query, parameters);
                return result?.ToString() ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}