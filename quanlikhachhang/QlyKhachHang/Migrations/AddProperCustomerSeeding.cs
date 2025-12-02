using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QlyKhachHang.Migrations
{
    public partial class AddProperCustomerSeeding : Migration
    {
        // Helper method to hash passwords
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing customer seed data
            migrationBuilder.Sql("DELETE FROM [Customers]");

            // Insert customers with proper password hashing
            // Password: 123456
            string hash123456 = HashPassword("123456");
            // Password: MatKhauMoi_123
            string hashMatkhu = HashPassword("MatKhauMoi_123");

            // Insert admin-like customer
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "Phone", "Email", "Address", "City", "PostalCode", "Username", "PasswordHash", "Status", "CreatedDate", "ModifiedDate", "LastLoginDate" },
                values: new object[] { 1, "Nguyễn Văn An", "0901234567", "kh1@gmail.com", "123 Đường 1, TP.HCM", "TP.HCM", "70000", "kh1", hash123456, "Active", new DateTime(2025, 11, 24, 14, 10, 19), DBNull.Value, DBNull.Value }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "Phone", "Email", "Address", "City", "PostalCode", "Username", "PasswordHash", "Status", "CreatedDate", "ModifiedDate", "LastLoginDate" },
                values: new object[] { 2, "Trần Thị Bò", "0909876543", "kh2@gmail.com", "123 Đường 2, TP.HCM", "TP.HCM", "70000", "kh2", hash123456, "Active", new DateTime(2025, 11, 24, 14, 10, 19), DBNull.Value, DBNull.Value }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "Phone", "Email", "Address", "City", "PostalCode", "Username", "PasswordHash", "Status", "CreatedDate", "ModifiedDate", "LastLoginDate" },
                values: new object[] { 3, "Phạm Minh Tuấn", "0912345678", "kh3@gmail.com", "123 Đường 3, TP.HCM", "TP.HCM", "70000", "kh3", hashMatkhu, "Active", new DateTime(2025, 11, 24, 14, 10, 19), DBNull.Value, DBNull.Value }
            );

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName", "Phone", "Email", "Address", "City", "PostalCode", "Username", "PasswordHash", "Status", "CreatedDate", "ModifiedDate", "LastLoginDate" },
                values: new object[] { 4, "Hoàng Ngân Giang", "0923456789", "kh4@gmail.com", "123 Đường 4, TP.HCM", "TP.HCM", "70000", "kh4", hash123456, "Active", new DateTime(2025, 11, 24, 14, 10, 19), DBNull.Value, DBNull.Value }
            );

            // Insert remaining customers (5-50) with password "123456"
            for (int i = 5; i <= 50; i++)
            {
                migrationBuilder.InsertData(
                    table: "Customers",
                    columns: new[] { "CustomerId", "CustomerName", "Phone", "Email", "Address", "City", "PostalCode", "Username", "PasswordHash", "Status", "CreatedDate", "ModifiedDate", "LastLoginDate" },
                    values: new object[] { 
                        i, 
                        $"Khách Hàng {i}", 
                        $"090{i:000000}", 
                        $"kh{i}@gmail.com", 
                        $"123 Đường {i}, TP.HCM", 
                        "TP.HCM", 
                        "70000", 
                        $"kh{i}", 
                        hash123456, 
                        "Active", 
                        new DateTime(2025, 11, 24, 14, 10, 19), 
                        DBNull.Value, 
                        DBNull.Value 
                    }
                );
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Customers]");
        }
    }
}
