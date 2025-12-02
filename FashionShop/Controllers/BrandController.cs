using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using FashionShop.Helper;
using FashionShop.Models;
using FashionShop.Models.ViewModels;

namespace FashionShop.Controllers
{
    public class BrandController : Controller
    {
        // GET: Admin/Brand
        public ActionResult Index()
        {
            try
            {
                var model = new BrandFormModel
                {
                    Form = new HangThoiTrang(),
                    List = GetAllBrands()
                };
                return View("~/Views/Brand/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading brands: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi tải dữ liệu: " + ex.Message
                };
                return View("~/Views/Brand/Index.cshtml", new BrandFormModel());
            }
        }

        // POST: Brand/AddOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(BrandFormModel model, HttpPostedFileBase LogoFile)
        {
            if (!ModelState.IsValid)
            {
                model.List = GetAllBrands();
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Dữ liệu không hợp lệ."
                };
                return View("~/Views/Brand/Index.cshtml", model);
            }

            try
            {
                var brand = model.Form;

                if (LogoFile != null && LogoFile.ContentLength > 0)
                {
                    // Validate file size (2MB max)
                    if (LogoFile.ContentLength > 2 * 1024 * 1024)
                    {
                        model.List = GetAllBrands();
                        TempData["Message"] = new AlertMessage
                        {
                            Type = "error",
                            Text = "Kích thước file quá lớn! Vui lòng chọn file nhỏ hơn 2MB."
                        };
                        return View("~/Views/Brand/Index.cshtml", model);
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(LogoFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        model.List = GetAllBrands();
                        TempData["Message"] = new AlertMessage
                        {
                            Type = "error",
                            Text = "Chỉ chấp nhận file hình ảnh (JPG, PNG, GIF)."
                        };
                        return View("~/Views/Brand/Index.cshtml", model);
                    }

                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string uploadPath = Server.MapPath("~/Uploads/Brands/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    LogoFile.SaveAs(filePath);

                    brand.Logo = "/Uploads/Brands/" + fileName;
                }

                bool isUpdate = brand.MaHang > 0 && CheckBrandExists(brand.MaHang);

                if (isUpdate)
                {
                    if (LogoFile == null || LogoFile.ContentLength == 0)
                    {
                        var existingBrand = GetBrandById(brand.MaHang);
                        brand.Logo = existingBrand.Logo;
                    }
                    UpdateBrand(brand);
                }
                else
                {
                    InsertBrand(brand);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database error: " + ex.Message);
                model.List = GetAllBrands();
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi lưu dữ liệu: " + ex.Message
                };
                return View("~/Views/Brand/Index.cshtml", model);
            }
        }

        // GET: Brand/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var brand = GetBrandById(id);
                if (brand == null)
                {
                    TempData["Message"] = new AlertMessage
                    {
                        Type = "error",
                        Text = "Không tìm thấy hãng thời trang"
                    };
                    return RedirectToAction("Index");
                }

                var model = new BrandFormModel
                {
                    Form = brand,
                    List = GetAllBrands()
                };
                return View("~/Views/Brand/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading brand for edit: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi tải dữ liệu."
                };
                return RedirectToAction("Index");
            }
        }

        // GET: Brand/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                // Get brand to delete logo file
                var brand = GetBrandById(id);
                if (brand != null && !string.IsNullOrEmpty(brand.Logo))
                {
                    // Delete logo file from server
                    string logoPath = Server.MapPath("~" + brand.Logo);
                    if (System.IO.File.Exists(logoPath))
                    {
                        System.IO.File.Delete(logoPath);
                    }
                }

                DeleteBrand(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting brand: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = ex.Message
                };
                return RedirectToAction("Index");
            }
        }

        // GET: Brand/Clear 
        public ActionResult Clear()
        {
            return RedirectToAction("Index");
        }

         //============================================
         //PRIVATE METHODS
         //============================================

        private List<HangThoiTrang> GetAllBrands()
        {
            List<HangThoiTrang> brands = new List<HangThoiTrang>();
            SqlParameter[] parameters = {
                new SqlParameter("@PageNumber", 1),
                new SqlParameter("@PageSize", 1000),
                new SqlParameter("@Keyword", DBNull.Value),
                new SqlParameter("@CountryOfOrigin", DBNull.Value),
                new SqlParameter("@IsActive", DBNull.Value)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetBrandList", parameters);

            foreach (DataRow row in dt.Rows)
            {
                brands.Add(new HangThoiTrang
                {
                    MaHang = Convert.ToInt32(row["Brand_Id"]),
                    TenHang = row["Name"].ToString(),
                    MoTa = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    XuatXu = row["CountryOfOrigin"] != DBNull.Value ? row["CountryOfOrigin"].ToString() : null,
                    Website = row["Website"] != DBNull.Value ? row["Website"].ToString() : null,
                    Logo = row["LogoUrl"] != DBNull.Value ? row["LogoUrl"].ToString() : null,
                    TrangThai = Convert.ToBoolean(row["IsActive"])
                });
            }
            return brands;
        }

        private HangThoiTrang GetBrandById(int maHang)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Brand_Id", maHang)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetBrandDetail", parameters);

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new HangThoiTrang
            {
                MaHang = Convert.ToInt32(row["Brand_Id"]),
                TenHang = row["Name"].ToString(),
                MoTa = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                XuatXu = row["CountryOfOrigin"] != DBNull.Value ? row["CountryOfOrigin"].ToString() : null,
                Website = row["Website"] != DBNull.Value ? row["Website"].ToString() : null,
                Logo = row["LogoUrl"] != DBNull.Value ? row["LogoUrl"].ToString() : null,
                TrangThai = Convert.ToBoolean(row["IsActive"])
            };
        }

        private void InsertBrand(HangThoiTrang brand)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Name", brand.TenHang),
                new SqlParameter("@Description", (object)brand.MoTa ?? DBNull.Value),
                new SqlParameter("@CountryOfOrigin", (object)brand.XuatXu ?? DBNull.Value),
                new SqlParameter("@Website", (object)brand.Website ?? DBNull.Value),
                new SqlParameter("@LogoUrl", (object)brand.Logo ?? DBNull.Value),
                new SqlParameter("@IsActive", brand.TrangThai)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_AddBrand", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();

                if (status == "SUCCESS")
                {
                    TempData["Message"] = new AlertMessage
                    {
                        Type = "success",
                        Text = message
                    };
                }
                else
                {
                    throw new Exception(message);
                }
            }
        }

        private void UpdateBrand(HangThoiTrang brand)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Brand_Id", brand.MaHang),
                new SqlParameter("@Name", brand.TenHang),
                new SqlParameter("@Description", (object)brand.MoTa ?? DBNull.Value),
                new SqlParameter("@CountryOfOrigin", (object)brand.XuatXu ?? DBNull.Value),
                new SqlParameter("@Website", (object)brand.Website ?? DBNull.Value),
                new SqlParameter("@LogoUrl", (object)brand.Logo ?? DBNull.Value),
                new SqlParameter("@IsActive", brand.TrangThai)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_UpdateBrand", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();

                if (status == "SUCCESS")
                {
                    TempData["Message"] = new AlertMessage
                    {
                        Type = "success",
                        Text = message
                    };
                }
                else
                {
                    throw new Exception(message);
                }
            }
        }

        private void DeleteBrand(int maHang)
        {
            SqlParameter[] parameters = {
        new SqlParameter("@Brand_Id", maHang)
    };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_DeleteBrand", parameters);

            if (dt.Rows.Count > 0)
            {
                string status = dt.Rows[0]["Status"].ToString();
                string message = dt.Rows[0]["Message"].ToString();

                if (status == "SUCCESS")
                {
                    TempData["Message"] = new AlertMessage
                    {
                        Type = "success",
                        Text = message
                    };
                }
                else
                {
                    throw new Exception(message);
                }
            }
        }

        private bool CheckBrandExists(int maHang)
        {
            try
            {
                var brand = GetBrandById(maHang);
                return brand != null;
            }
            catch
            {
                return false;
            }
        }
    }
}