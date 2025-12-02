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
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var model = new ProductFormModel
                {
                    Form = new Product(),
                    List = GetAllProducts(),
                    Categories = GetAllCategories(),
                    Brands = GetAllBrands(),
                    Colors = GetAllColors(),
                    Sizes = GetAllSizes()
                };
                return View("~/Views/Product/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading products: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi tải dữ liệu: " + ex.Message
                };
                return View("~/Views/Product/Index.cshtml", new ProductFormModel());
            }
        }

        // POST: Product/AddOrUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrUpdate(ProductFormModel model, HttpPostedFileBase ImageFile)
        {
            if (!ModelState.IsValid)
            {
                model.List = GetAllProducts();
                model.Categories = GetAllCategories();
                model.Brands = GetAllBrands();
                model.Colors = GetAllColors();
                model.Sizes = GetAllSizes();
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Dữ liệu không hợp lệ."
                };
                return View("~/Views/Product/Index.cshtml", model);
            }

            try
            {
                var product = model.Form;

                // Handle image upload
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    if (ImageFile.ContentLength > 5 * 1024 * 1024)
                    {
                        model.List = GetAllProducts();
                        model.Categories = GetAllCategories();
                        model.Brands = GetAllBrands();
                        model.Colors = GetAllColors();
                        model.Sizes = GetAllSizes();
                        TempData["Message"] = new AlertMessage
                        {
                            Type = "error",
                            Text = "Kích thước file quá lớn! Vui lòng chọn file nhỏ hơn 5MB."
                        };
                        return View("~/Views/Product/Index.cshtml", model);
                    }

                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        model.List = GetAllProducts();
                        model.Categories = GetAllCategories();
                        model.Brands = GetAllBrands();
                        model.Colors = GetAllColors();
                        model.Sizes = GetAllSizes();
                        TempData["Message"] = new AlertMessage
                        {
                            Type = "error",
                            Text = "Chỉ chấp nhận file hình ảnh (JPG, PNG, GIF, WEBP)."
                        };
                        return View("~/Views/Product/Index.cshtml", model);
                    }

                    string fileName = Guid.NewGuid().ToString() + fileExtension;
                    string uploadPath = Server.MapPath("~/Uploads/Products/");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    ImageFile.SaveAs(filePath);

                    product.HinhAnh = "/Uploads/Products/" + fileName;
                }

                bool isUpdate = product.MaSP > 0 && CheckProductExists(product.MaSP);

                if (isUpdate)
                {
                    if (ImageFile == null || ImageFile.ContentLength == 0)
                    {
                        var existingProduct = GetProductById(product.MaSP);
                        product.HinhAnh = existingProduct.HinhAnh;
                    }
                    UpdateProduct(product);
                }
                else
                {
                    InsertProduct(product);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Database error: " + ex.Message);
                model.List = GetAllProducts();
                model.Categories = GetAllCategories();
                model.Brands = GetAllBrands();
                model.Colors = GetAllColors();
                model.Sizes = GetAllSizes();
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi lưu dữ liệu: " + ex.Message
                };
                return View("~/Views/Product/Index.cshtml", model);
            }
        }

        // GET: Product/Edit
        public ActionResult Edit(int id)
        {
            try
            {
                var product = GetProductById(id);
                if (product == null)
                {
                    TempData["Message"] = new AlertMessage
                    {
                        Type = "error",
                        Text = "Không tìm thấy sản phẩm"
                    };
                    return RedirectToAction("Index");
                }

                var model = new ProductFormModel
                {
                    Form = product,
                    List = GetAllProducts(),
                    Categories = GetAllCategories(),
                    Brands = GetAllBrands(),
                    Colors = GetAllColors(),
                    Sizes = GetAllSizes()
                };
                return View("~/Views/Product/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading product for edit: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = "Có lỗi khi tải dữ liệu."
                };
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Delete
        public ActionResult Delete(int id)
        {
            try
            {
                var product = GetProductById(id);
                if (product != null && !string.IsNullOrEmpty(product.HinhAnh))
                {
                    string imagePath = Server.MapPath("~" + product.HinhAnh);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting product: " + ex.Message);
                TempData["Message"] = new AlertMessage
                {
                    Type = "error",
                    Text = ex.Message
                };
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Clear
        public ActionResult Clear()
        {
            return RedirectToAction("Index");
        }


        // PRIVATE METHOD

        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            SqlParameter[] parameters = {
                new SqlParameter("@PageNumber", 1),
                new SqlParameter("@PageSize", 1000),
                new SqlParameter("@Keyword", DBNull.Value),
                new SqlParameter("@Category_Id", DBNull.Value),
                new SqlParameter("@Brand_Id", DBNull.Value),
                new SqlParameter("@IsActive", DBNull.Value)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetProductList", parameters);

            foreach (DataRow row in dt.Rows)
            {
                products.Add(new Product
                {
                    MaSP = Convert.ToInt32(row["Product_Id"]),
                    TenSP = row["ProductName"].ToString(),
                    MoTaNgan = row["ShortDescription"] != DBNull.Value ? row["ShortDescription"].ToString() : null,
                    Gia = Convert.ToDecimal(row["Price"]),
                    GiaKM = row["SalePrice"] != DBNull.Value ? Convert.ToDecimal(row["SalePrice"]) : (decimal?)null,
                    SoLuong = Convert.ToInt32(row["Quantity"]),
                    TrangThai = Convert.ToBoolean(row["IsActive"]),
                    HinhAnh = row["ImageUrl"] != DBNull.Value ? row["ImageUrl"].ToString() : null,
                    TenDanhMuc = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : null,
                    TenHang = row["BrandName"] != DBNull.Value ? row["BrandName"].ToString() : null,
                    MaDanhMuc = row["Category_Id"] != DBNull.Value ? Convert.ToInt32(row["Category_Id"]) : 0,
                    MaHang = row["Brand_Id"] != DBNull.Value ? Convert.ToInt32(row["Brand_Id"]) : (int?)null
                });
            }
            return products;
        }

        private Product GetProductById(int maSP)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Product_Id", maSP)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetProductDetail", parameters);

            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new Product
            {
                MaSP = Convert.ToInt32(row["Product_Id"]),
                TenSP = row["ProductName"].ToString(),
                MoTaNgan = row["ShortDescription"] != DBNull.Value ? row["ShortDescription"].ToString() : null,
                MoTaChiTiet = row["FullDescription"] != DBNull.Value ? row["FullDescription"].ToString() : null,
                Gia = Convert.ToDecimal(row["Price"]),
                GiaKM = row["SalePrice"] != DBNull.Value ? Convert.ToDecimal(row["SalePrice"]) : (decimal?)null,
                SoLuong = Convert.ToInt32(row["Quantity"]),
                TrangThai = Convert.ToBoolean(row["IsActive"]),
                HinhAnh = row["ImageUrl"] != DBNull.Value ? row["ImageUrl"].ToString() : null,
                MaDanhMuc = Convert.ToInt32(row["Category_Id"]),
                MaHang = row["Brand_Id"] != DBNull.Value ? Convert.ToInt32(row["Brand_Id"]) : (int?)null,
                MaMau = row["Color_Id"] != DBNull.Value ? Convert.ToInt32(row["Color_Id"]) : (int?)null,
                MaSize = row["Size_Id"] != DBNull.Value ? Convert.ToInt32(row["Size_Id"]) : (int?)null
            };
        }

        private void InsertProduct(Product product)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ProductName", product.TenSP),
                new SqlParameter("@ShortDescription", (object)product.MoTaNgan ?? DBNull.Value),
                new SqlParameter("@FullDescription", (object)product.MoTaChiTiet ?? DBNull.Value),
                new SqlParameter("@Price", product.Gia),
                new SqlParameter("@SalePrice", (object)product.GiaKM ?? DBNull.Value),
                new SqlParameter("@Quantity", product.SoLuong),
                new SqlParameter("@IsActive", product.TrangThai),
                new SqlParameter("@ImageUrl", (object)product.HinhAnh ?? DBNull.Value),
                new SqlParameter("@Category_Id", product.MaDanhMuc),
                new SqlParameter("@Brand_Id", (object)product.MaHang ?? DBNull.Value),
                new SqlParameter("@Color_Id", (object)product.MaMau ?? DBNull.Value),
                new SqlParameter("@Size_Id", (object)product.MaSize ?? DBNull.Value)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_AddProduct", parameters);

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

        private void UpdateProduct(Product product)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Product_Id", product.MaSP),
                new SqlParameter("@ProductName", product.TenSP),
                new SqlParameter("@ShortDescription", (object)product.MoTaNgan ?? DBNull.Value),
                new SqlParameter("@FullDescription", (object)product.MoTaChiTiet ?? DBNull.Value),
                new SqlParameter("@Price", product.Gia),
                new SqlParameter("@SalePrice", (object)product.GiaKM ?? DBNull.Value),
                new SqlParameter("@Quantity", product.SoLuong),
                new SqlParameter("@IsActive", product.TrangThai),
                new SqlParameter("@ImageUrl", (object)product.HinhAnh ?? DBNull.Value),
                new SqlParameter("@Category_Id", product.MaDanhMuc),
                new SqlParameter("@Brand_Id", (object)product.MaHang ?? DBNull.Value),
                new SqlParameter("@Color_Id", (object)product.MaMau ?? DBNull.Value),
                new SqlParameter("@Size_Id", (object)product.MaSize ?? DBNull.Value)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_UpdateProduct", parameters);

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

        private void DeleteProduct(int maSP)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Product_Id", maSP)
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_DeleteProduct", parameters);

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

        private bool CheckProductExists(int maSP)
        {
            try
            {
                var product = GetProductById(maSP);
                return product != null;
            }
            catch
            {
                return false;
            }
        }

        private List<SelectListItem> GetAllCategories()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            DataTable dt = DatabaseHelper.ExecuteQuery("SELECT Category_Id, CategoryName FROM Category WHERE IsActive = 1 ORDER BY CategoryName");

            foreach (DataRow row in dt.Rows)
            {
                categories.Add(new SelectListItem
                {
                    Value = row["Category_Id"].ToString(),
                    Text = row["CategoryName"].ToString()
                });
            }
            return categories;
        }

        private List<SelectListItem> GetAllBrands()
        {
            List<SelectListItem> brands = new List<SelectListItem>();
            DataTable dt = DatabaseHelper.ExecuteQuery("SELECT Brand_Id, Name FROM Brand WHERE IsActive = 1 ORDER BY Name");

            foreach (DataRow row in dt.Rows)
            {
                brands.Add(new SelectListItem
                {
                    Value = row["Brand_Id"].ToString(),
                    Text = row["Name"].ToString()
                });
            }
            return brands;
        }

        private List<SelectListItem> GetAllColors()
        {
            List<SelectListItem> colors = new List<SelectListItem>();
            DataTable dt = DatabaseHelper.ExecuteQuery("SELECT Color_Id, ColorName FROM Color ORDER BY ColorName");

            foreach (DataRow row in dt.Rows)
            {
                colors.Add(new SelectListItem
                {
                    Value = row["Color_Id"].ToString(),
                    Text = row["ColorName"].ToString()
                });
            }
            return colors;
        }

        private List<SelectListItem> GetAllSizes()
        {
            List<SelectListItem> sizes = new List<SelectListItem>();
            DataTable dt = DatabaseHelper.ExecuteQuery("SELECT Size_Id, SizeName FROM Size ORDER BY SortOrder");

            foreach (DataRow row in dt.Rows)
            {
                sizes.Add(new SelectListItem
                {
                    Value = row["Size_Id"].ToString(),
                    Text = row["SizeName"].ToString()
                });
            }
            return sizes;
        }
    }
}