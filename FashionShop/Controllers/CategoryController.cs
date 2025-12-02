using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using FashionShop.Models;
using FashionShop.Models.ViewModels;
using System.Collections.Generic;
using System.Configuration;

namespace FashionShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["FashionShopDb"].ConnectionString;

        // GET: Category Management Page
        public ActionResult Category(int page = 1, int pageSize = 10)
        {
            var model = new CategoryFormModel();
            model.Form = new Category();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy danh sách categories với phân trang
                    using (SqlCommand cmd = new SqlCommand("sp_Category_GetList", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageNumber", page);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            model.List = new List<Category>();

                            while (reader.Read())
                            {
                                var category = new Category
                                {
                                    Category_Id = Convert.ToInt32(reader["Category_Id"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    CategorySlug = reader["CategorySlug"].ToString(),
                                    Description = reader["Description"]?.ToString(),
                                    CategoryImageUrl = reader["CategoryImageUrl"]?.ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    ParentCategory_Id = reader["ParentCategory_Id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ParentCategory_Id"]) : null,
                                    Brand_Id = reader["Brand_Id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Brand_Id"]) : null,
                                    IsFeatured = reader["IsFeatured"] != DBNull.Value ? Convert.ToBoolean(reader["IsFeatured"]) : false,
                                    SortOrder = reader["SortOrder"] != DBNull.Value ? Convert.ToInt32(reader["SortOrder"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = reader["CreatedBy"]?.ToString(),
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ModifiedDate"]) : null,
                                    ModifiedBy = reader["ModifiedBy"]?.ToString()
                                };

                                // Thêm thông tin Brand nếu có
                                if (reader["BrandName"] != DBNull.Value)
                                {
                                    category.Brand = new HangThoiTrang
                                    {
                                        MaHang = Convert.ToInt32(reader["Brand_Id"]),
                                        TenHang = reader["BrandName"].ToString()
                                    };
                                }

                                model.List.Add(category);
                            }

                            // Đọc tổng số records
                            if (reader.NextResult() && reader.Read())
                            {
                                ViewBag.TotalItems = Convert.ToInt32(reader["TotalCategories"]);
                                ViewBag.TotalPages = (int)Math.Ceiling((double)ViewBag.TotalItems / pageSize);
                            }
                        }
                    }

                    // Lấy danh sách Brands cho dropdown
                    using (SqlCommand cmd = new SqlCommand("sp_GetActiveBrands", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            model.Brands = new List<HangThoiTrang>();

                            while (reader.Read())
                            {
                                model.Brands.Add(new HangThoiTrang
                                {
                                    MaHang = Convert.ToInt32(reader["Brand_Id"]),
                                    TenHang = reader["Name"].ToString()
                                });
                            }
                        }
                    }
                }

                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                model.List = new List<Category>();
                model.Brands = new List<HangThoiTrang>();
                ViewBag.CurrentPage = 1;
                ViewBag.TotalPages = 1;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = 0;
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }

            return View("~/Views/Category/Category.cshtml", model);
        }

        // POST: Thêm mới hoặc cập nhật Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(CategoryFormModel model)
        {
            if (model?.Form == null)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
                return RedirectToAction("Category");
            }

            var category = model.Form;

            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    TempData["ErrorMessage"] = "Tên danh mục không được để trống.";
                    return RedirectToAction("Category");
                }

                // Tạo slug tự động nếu chưa có
                if (string.IsNullOrWhiteSpace(category.CategorySlug))
                {
                    category.CategorySlug = GenerateSlug(category.CategoryName);
                }

                string newImageUrl = category.CategoryImageUrl;

                // Process image upload
                if (category.ImageFile != null && category.ImageFile.ContentLength > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(category.ImageFile.FileName)?.ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        TempData["ErrorMessage"] = "Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif)";
                        return RedirectToAction("Category");
                    }

                    if (category.ImageFile.ContentLength > 5 * 1024 * 1024)
                    {
                        TempData["ErrorMessage"] = "Kích thước file không được vượt quá 5MB";
                        return RedirectToAction("Category");
                    }

                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string folderPath = Server.MapPath("~/Uploads/Categories/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);
                    category.ImageFile.SaveAs(fullPath);
                    newImageUrl = "/Uploads/Categories/" + fileName;

                    // Xóa ảnh cũ nếu đang cập nhật
                    if (category.Category_Id > 0 && !string.IsNullOrEmpty(category.CategoryImageUrl))
                    {
                        string oldImagePath = Server.MapPath("~" + category.CategoryImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                // Gọi Stored Procedure
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_Category_AddOrUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Category_Id", category.Category_Id);
                        cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                        cmd.Parameters.AddWithValue("@CategorySlug", category.CategorySlug);
                        cmd.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CategoryImageUrl", (object)newImageUrl ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                        cmd.Parameters.AddWithValue("@ParentCategory_Id", (object)category.ParentCategory_Id ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Brand_Id", (object)category.Brand_Id ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ActionBy", User.Identity.Name ?? "Admin");

                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);

                        SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(messageParam);

                        cmd.ExecuteNonQuery();

                        int result = Convert.ToInt32(resultParam.Value);
                        string message = messageParam.Value.ToString();

                        if (result == 1)
                        {
                            TempData["SuccessMessage"] = message;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
            }

            return RedirectToAction("Category");
        }

        // GET: Edit Category
        public ActionResult Edit(int id)
        {
            var model = new CategoryFormModel();
            model.Brands = new List<HangThoiTrang>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy thông tin category
                    using (SqlCommand cmd = new SqlCommand("sp_Category_GetById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Category_Id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                model.Form = new Category
                                {
                                    Category_Id = Convert.ToInt32(reader["Category_Id"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    CategorySlug = reader["CategorySlug"].ToString(),
                                    Description = reader["Description"]?.ToString(),
                                    CategoryImageUrl = reader["CategoryImageUrl"]?.ToString(),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    ParentCategory_Id = reader["ParentCategory_Id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["ParentCategory_Id"]) : null,
                                    Brand_Id = reader["Brand_Id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Brand_Id"]) : null,
                                    IsFeatured = reader["IsFeatured"] != DBNull.Value ? Convert.ToBoolean(reader["IsFeatured"]) : false,
                                    SortOrder = reader["SortOrder"] != DBNull.Value ? Convert.ToInt32(reader["SortOrder"]) : 0,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : DateTime.Now,
                                    CreatedBy = reader["CreatedBy"]?.ToString(),
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ModifiedDate"]) : null,
                                    ModifiedBy = reader["ModifiedBy"]?.ToString()
                                };
                            }
                        }
                    }

                    // Lấy danh sách Brands
                    using (SqlCommand cmd = new SqlCommand("sp_GetActiveBrands", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.Brands.Add(new HangThoiTrang
                                {
                                    MaHang = Convert.ToInt32(reader["Brand_Id"]),
                                    TenHang = reader["Name"].ToString()
                                });
                            }
                        }
                    }
                }

                model.List = new List<Category>();
                return View("~/Views/Category/Category.cshtml", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Không thể tải dữ liệu: " + ex.Message;
                return RedirectToAction("Category");
            }
        }

        // POST: Delete Category
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_Category_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Category_Id", id);
                        cmd.Parameters.AddWithValue("@DeletedBy", User.Identity.Name ?? "Admin");

                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultParam);

                        SqlParameter messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(messageParam);

                        cmd.ExecuteNonQuery();

                        int result = Convert.ToInt32(resultParam.Value);
                        string message = messageParam.Value.ToString();

                        if (result == 1)
                        {
                            TempData["SuccessMessage"] = message;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa: " + ex.Message;
            }

            return RedirectToAction("Category");
        }

        // Helper method: Generate slug from Vietnamese text
        private string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            // Chuyển về chữ thường
            text = text.ToLower();

            // Bỏ dấu tiếng Việt
            text = RemoveVietnameseTones(text);

            // Thay thế khoảng trắng bằng dấu gạch ngang
            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "-");

            // Bỏ các ký tự đặc biệt
            text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\-]", "");

            // Bỏ dấu gạch ngang thừa
            text = System.Text.RegularExpressions.Regex.Replace(text, @"-+", "-");

            return text.Trim('-');
        }

        private string RemoveVietnameseTones(string text)
        {
            string[] vietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                {
                    text = text.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            return text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Cleanup nếu cần
            }
            base.Dispose(disposing);
        }
    }
}