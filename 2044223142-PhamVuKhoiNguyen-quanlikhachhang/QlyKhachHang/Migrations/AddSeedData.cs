 using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QlyKhachHang.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Delete existing seed data first
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            // Insert 50 Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "CreatedDate", "Description", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 1, "Áo", new DateTime(2025, 11, 24, 14, 10, 19), "Áo thể thao Nike Air chất lượng cao", 500000m, "Áo Nike Air", 100 },
                    { 2, "Áo", new DateTime(2025, 11, 24, 14, 10, 19), "Áo thể thao Adidas Sport", 450000m, "Áo Adidas Sport", 150 },
                    { 3, "Áo", new DateTime(2025, 11, 24, 14, 10, 19), "Áo chạy bộ Puma Run", 480000m, "Áo Puma Run", 120 },
                    { 4, "Áo", new DateTime(2025, 11, 24, 14, 10, 19), "Áo cổ điển Reebok Classic", 420000m, "Áo Reebok Classic", 80 },
                    { 5, "Áo", new DateTime(2025, 11, 24, 14, 10, 19), "Áo chuyên nghiệp New Balance Pro", 520000m, "Áo New Balance Pro", 95 },
                    { 6, "Quần", new DateTime(2025, 11, 24, 14, 10, 19), "Quần Nike Flex thoáng khí", 350000m, "Quần Nike Flex", 110 },
                    { 7, "Quần", new DateTime(2025, 11, 24, 14, 10, 19), "Quần tập luyện Adidas Training", 380000m, "Quần Adidas Training", 100 },
                    { 8, "Quần", new DateTime(2025, 11, 24, 14, 10, 19), "Quần thoải mái Puma Comfort", 360000m, "Quần Puma Comfort", 130 },
                    { 9, "Quần", new DateTime(2025, 11, 24, 14, 10, 19), "Quần thể thao Reebok Sport", 340000m, "Quần Reebok Sport", 105 },
                    { 10, "Quần", new DateTime(2025, 11, 24, 14, 10, 19), "Quần năng động New Balance Active", 390000m, "Quần New Balance Active", 85 },
                    { 11, "Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Giày chạy Nike Runner", 750000m, "Giày Nike Runner", 60 },
                    { 12, "Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Giày cao cấp Adidas Ultra", 800000m, "Giày Adidas Ultra", 50 },
                    { 13, "Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Giày tốc độ Puma Velocity", 720000m, "Giày Puma Velocity", 70 },
                    { 14, "Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Giày bão Reebok Storm", 680000m, "Giày Reebok Storm", 75 },
                    { 15, "Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Giày tươi mới New Balance Fresh", 820000m, "Giày New Balance Fresh", 55 },
                    { 16, "Mũ", new DateTime(2025, 11, 24, 14, 10, 19), "Mũ cổ điển Nike Classic", 150000m, "Mũ Nike Classic", 200 },
                    { 17, "Mũ", new DateTime(2025, 11, 24, 14, 10, 19), "Mũ thể thao Adidas Sport", 160000m, "Mũ Adidas Sport", 180 },
                    { 18, "Mũ", new DateTime(2025, 11, 24, 14, 10, 19), "Mũ chuyên nghiệp Puma Pro", 155000m, "Mũ Puma Pro", 190 },
                    { 19, "Mũ", new DateTime(2025, 11, 24, 14, 10, 19), "Mũ năng động Reebok Active", 145000m, "Mũ Reebok Active", 210 },
                    { 20, "Mũ", new DateTime(2025, 11, 24, 14, 10, 19), "Mũ thời trang New Balance Fashion", 165000m, "Mũ New Balance Fashion", 170 },
                    { 21, "Tất", new DateTime(2025, 11, 24, 14, 10, 19), "Tất thoải mái Nike Comfort", 80000m, "Tất Nike Comfort", 300 },
                    { 22, "Tất", new DateTime(2025, 11, 24, 14, 10, 19), "Tất cao cấp Adidas Premium", 90000m, "Tất Adidas Premium", 280 },
                    { 23, "Tất", new DateTime(2025, 11, 24, 14, 10, 19), "Tất tinh hoa Puma Elite", 85000m, "Tất Puma Elite", 290 },
                    { 24, "Tất", new DateTime(2025, 11, 24, 14, 10, 19), "Tất thể thao Reebok Sport", 75000m, "Tất Reebok Sport", 310 },
                    { 25, "Tất", new DateTime(2025, 11, 24, 14, 10, 19), "Tất công nghệ New Balance Tech", 95000m, "Tất New Balance Tech", 270 },
                    { 26, "Áo Khoác", new DateTime(2025, 11, 24, 14, 10, 19), "Áo khoác gió Nike Wind", 650000m, "Áo Khoác Nike Wind", 75 },
                    { 27, "Áo Khoác", new DateTime(2025, 11, 24, 14, 10, 19), "Áo khoác tăng cường Adidas Boost", 700000m, "Áo Khoác Adidas Boost", 65 },
                    { 28, "Áo Khoác", new DateTime(2025, 11, 24, 14, 10, 19), "Áo khoác mãnh liệt Puma Fierce", 620000m, "Áo Khoác Puma Fierce", 80 },
                    { 29, "Áo Khoác", new DateTime(2025, 11, 24, 14, 10, 19), "Áo khoác bão Reebok Storm", 580000m, "Áo Khoác Reebok Storm", 90 },
                    { 30, "Áo Khoác", new DateTime(2025, 11, 24, 14, 10, 19), "Áo khoác nhiệt New Balance Heat", 720000m, "Áo Khoác New Balance Heat", 70 },
                    { 31, "Thắt Lưng", new DateTime(2025, 11, 24, 14, 10, 19), "Thắt lưng chuyên nghiệp Nike Pro", 120000m, "Thắt Lưng Nike Pro", 220 },
                    { 32, "Thắt Lưng", new DateTime(2025, 11, 24, 14, 10, 19), "Thắt lưng cổ điển Adidas Classic", 130000m, "Thắt Lưng Adidas Classic", 200 },
                    { 33, "Thắt Lưng", new DateTime(2025, 11, 24, 14, 10, 19), "Thắt lưng năng động Puma Active", 125000m, "Thắt Lưng Puma Active", 210 },
                    { 34, "Thắt Lưng", new DateTime(2025, 11, 24, 14, 10, 19), "Thắt lưng thể thao Reebok Sport", 115000m, "Thắt Lưng Reebok Sport", 230 },
                    { 35, "Thắt Lưng", new DateTime(2025, 11, 24, 14, 10, 19), "Thắt lưng New Balance", 135000m, "Thắt Lưng New Balance", 190 },
                    { 36, "Ba Lô", new DateTime(2025, 11, 24, 14, 10, 19), "Ba lô du lịch Nike Travel", 550000m, "Ba Lô Nike Travel", 55 },
                    { 37, "Ba Lô", new DateTime(2025, 11, 24, 14, 10, 19), "Ba lô đô thị Adidas Urban", 600000m, "Ba Lô Adidas Urban", 45 },
                    { 38, "Ba Lô", new DateTime(2025, 11, 24, 14, 10, 19), "Ba lô học sinh Puma School", 520000m, "Ba Lô Puma School", 65 },
                    { 39, "Ba Lô", new DateTime(2025, 11, 24, 14, 10, 19), "Ba lô khám phá Reebok Explorer", 480000m, "Ba Lô Reebok Explorer", 75 },
                    { 40, "Ba Lô", new DateTime(2025, 11, 24, 14, 10, 19), "Ba lô trekking New Balance Trek", 620000m, "Ba Lô New Balance Trek", 50 },
                    { 41, "Dây Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Dây giày chuyên nghiệp Nike Pro", 50000m, "Dây Giày Nike Pro", 400 },
                    { 42, "Dây Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Dây giày thể thao Adidas Sport", 55000m, "Dây Giày Adidas Sport", 380 },
                    { 43, "Dây Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Dây giày chắc chẽ Puma Strong", 52000m, "Dây Giày Puma Strong", 390 },
                    { 44, "Dây Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Dây giày linh hoạt Reebok Flex", 48000m, "Dây Giày Reebok Flex", 410 },
                    { 45, "Dây Giày", new DateTime(2025, 11, 24, 14, 10, 19), "Dây giày công nghệ New Balance Tech", 58000m, "Dây Giày New Balance Tech", 370 },
                    { 46, "Túi", new DateTime(2025, 11, 24, 14, 10, 19), "Túi đeo nhanh Nike Sprint", 250000m, "Túi Đeo Nike Sprint", 120 },
                    { 47, "Túi", new DateTime(2025, 11, 24, 14, 10, 19), "Túi đeo di chuyển Adidas Move", 280000m, "Túi Đeo Adidas Move", 100 },
                    { 48, "Túi", new DateTime(2025, 11, 24, 14, 10, 19), "Túi đeo nhanh chóng Puma Quick", 260000m, "Túi Đeo Puma Quick", 110 },
                    { 49, "Túi", new DateTime(2025, 11, 24, 14, 10, 19), "Túi đeo nhanh Reebok Fast", 240000m, "Túi Đeo Reebok Fast", 130 },
                    { 50, "Túi", new DateTime(2025, 11, 24, 14, 10, 19), "Túi đeo kết nối New Balance Connect", 290000m, "Túi Đeo New Balance Connect", 105 }
                });

            // Insert 50 Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "BirthDate", "City", "CreatedDate", "CustomerName", "Email", "Gender", "LastLoginDate", "ModifiedDate", "PasswordHash", "Phone", "PostalCode", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "123 Đường 1, TP.HCM", new DateTime(1990, 1, 1), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Nguyễn Văn An", "kh1@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0901234567", "70000", "Active", "kh1" },
                    { 2, "123 Đường 2, TP.HCM", new DateTime(1995, 5, 5), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Trần Thị Bò", "kh2@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0909876543", "70000", "Active", "kh2" },
                    { 3, "123 Đường 3, TP.HCM", new DateTime(1992, 3, 15), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phạm Minh Tuấn", "kh3@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0912345678", "70000", "Active", "kh3" },
                    { 4, "123 Đường 4, TP.HCM", new DateTime(1998, 7, 20), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Hoàng Ngân Giang", "kh4@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0923456789", "70000", "Active", "kh4" },
                    { 5, "123 Đường 5, TP.HCM", new DateTime(1988, 11, 10), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Võ Quốc Hùng", "kh5@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0934567890", "70000", "Active", "kh5" },
                    { 6, "123 Đường 6, TP.HCM", new DateTime(1994, 6, 25), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Dương Thị Hương", "kh6@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0945678901", "70000", "Active", "kh6" },
                    { 7, "123 Đường 7, TP.HCM", new DateTime(1991, 2, 14), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Bùi Anh Khoa", "kh7@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0956789012", "70000", "Active", "kh7" },
                    { 8, "123 Đường 8, TP.HCM", new DateTime(1996, 9, 3), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Lê Thị Thanh Hương", "kh8@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0967890123", "70000", "Active", "kh8" },
                    { 9, "123 Đường 9, TP.HCM", new DateTime(1993, 12, 8), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đặng Văn Tâm", "kh9@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0978901234", "70000", "Active", "kh9" },
                    { 10, "123 Đường 10, TP.HCM", new DateTime(1997, 4, 17), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Ngô Thị Hương Giang", "kh10@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0989012345", "70000", "Active", "kh10" },
                    { 11, "123 Đường 11, TP.HCM", new DateTime(1989, 8, 22), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Vương Quốc Cường", "kh11@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0901111111", "70000", "Active", "kh11" },
                    { 12, "123 Đường 12, TP.HCM", new DateTime(1999, 5, 30), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Nông Thị Liên", "kh12@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0902222222", "70000", "Active", "kh12" },
                    { 13, "123 Đường 13, TP.HCM", new DateTime(1990, 10, 12), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Trương Văn Hải", "kh13@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0903333333", "70000", "Active", "kh13" },
                    { 14, "123 Đường 14, TP.HCM", new DateTime(1993, 1, 28), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phan Thị Thu Thảo", "kh14@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0904444444", "70000", "Active", "kh14" },
                    { 15, "123 Đường 15, TP.HCM", new DateTime(1995, 7, 19), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Lý Văn Kiên", "kh15@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0905555555", "70000", "Active", "kh15" },
                    { 16, "123 Đường 16, TP.HCM", new DateTime(1992, 3, 4), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đinh Thị Kim Cương", "kh16@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0906666666", "70000", "Active", "kh16" },
                    { 17, "123 Đường 17, TP.HCM", new DateTime(1991, 9, 15), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Tôn Văn Mạnh", "kh17@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0907777777", "70000", "Active", "kh17" },
                    { 18, "123 Đường 18, TP.HCM", new DateTime(1996, 11, 6), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phan Thị Huyền", "kh18@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0908888888", "70000", "Active", "kh18" },
                    { 19, "123 Đường 19, TP.HCM", new DateTime(1988, 2, 20), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đỗ Văn Sơn", "kh19@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0909999999", "70000", "Active", "kh19" },
                    { 20, "123 Đường 20, TP.HCM", new DateTime(1994, 6, 11), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Vũ Thị Minh Châu", "kh20@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0910101010", "70000", "Active", "kh20" },
                    { 21, "123 Đường 21, TP.HCM", new DateTime(1990, 12, 25), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Kiều Văn Định", "kh21@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0911111111", "70000", "Active", "kh21" },
                    { 22, "123 Đường 22, TP.HCM", new DateTime(1997, 8, 14), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Trịnh Thị Mỹ Duyên", "kh22@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0912121212", "70000", "Active", "kh22" },
                    { 23, "123 Đường 23, TP.HCM", new DateTime(1989, 4, 7), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Cao Văn Hiếu", "kh23@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0913131313", "70000", "Active", "kh23" },
                    { 24, "123 Đường 24, TP.HCM", new DateTime(1993, 10, 19), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Bạch Thị Ngọc Anh", "kh24@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0914141414", "70000", "Active", "kh24" },
                    { 25, "123 Đường 25, TP.HCM", new DateTime(1991, 5, 23), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Thái Văn Phú", "kh25@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0915151515", "70000", "Active", "kh25" },
                    { 26, "123 Đường 26, TP.HCM", new DateTime(1995, 1, 9), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Tạ Thị Hồng Nhung", "kh26@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0916161616", "70000", "Active", "kh26" },
                    { 27, "123 Đường 27, TP.HCM", new DateTime(1992, 7, 31), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Hà Văn Công", "kh27@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0917171717", "70000", "Active", "kh27" },
                    { 28, "123 Đường 28, TP.HCM", new DateTime(1998, 3, 16), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Tô Thị Vân Anh", "kh28@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0918181818", "70000", "Active", "kh28" },
                    { 29, "123 Đường 29, TP.HCM", new DateTime(1990, 9, 2), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Vương Văn Tú", "kh29@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0919191919", "70000", "Active", "kh29" },
                    { 30, "123 Đường 30, TP.HCM", new DateTime(1994, 11, 27), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Quốc Thị Thanh Tâm", "kh30@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0920202020", "70000", "Active", "kh30" },
                    { 31, "123 Đường 31, TP.HCM", new DateTime(1989, 6, 13), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phan Văn Luân", "kh31@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0921212121", "70000", "Active", "kh31" },
                    { 32, "123 Đường 32, TP.HCM", new DateTime(1996, 2, 8), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Lê Thị Kim Yến", "kh32@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0922222222", "70000", "Active", "kh32" },
                    { 33, "123 Đường 33, TP.HCM", new DateTime(1991, 8, 21), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phạm Văn Chiến", "kh33@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0923232323", "70000", "Active", "kh33" },
                    { 34, "123 Đường 34, TP.HCM", new DateTime(1993, 4, 5), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đoàn Thị Hương Giang", "kh34@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0924242424", "70000", "Active", "kh34" },
                    { 35, "123 Đường 35, TP.HCM", new DateTime(1988, 10, 14), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Ngô Văn Hùng", "kh35@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0925252525", "70000", "Active", "kh35" },
                    { 36, "123 Đường 36, TP.HCM", new DateTime(1997, 7, 22), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Trần Thị Phương Liên", "kh36@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0926262626", "70000", "Active", "kh36" },
                    { 37, "123 Đường 37, TP.HCM", new DateTime(1992, 12, 30), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Hoàng Văn Dũng", "kh37@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0927272727", "70000", "Active", "kh37" },
                    { 38, "123 Đường 38, TP.HCM", new DateTime(1994, 9, 11), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Bùi Thị Hương Ly", "kh38@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0928282828", "70000", "Active", "kh38" },
                    { 39, "123 Đường 39, TP.HCM", new DateTime(1990, 5, 17), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Võ Văn Tuấn", "kh39@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0929292929", "70000", "Active", "kh39" },
                    { 40, "123 Đường 40, TP.HCM", new DateTime(1995, 11, 28), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Lê Thị Hương Thảo", "kh40@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0930303030", "70000", "Active", "kh40" },
                    { 41, "123 Đường 41, TP.HCM", new DateTime(1989, 3, 6), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đặng Văn Huy", "kh41@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0931313131", "70000", "Active", "kh41" },
                    { 42, "123 Đường 42, TP.HCM", new DateTime(1998, 8, 12), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Nguyễn Thị Hương Lan", "kh42@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0932323232", "70000", "Active", "kh42" },
                    { 43, "123 Đường 43, TP.HCM", new DateTime(1991, 1, 18), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Trương Văn Minh", "kh43@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0933333333", "70000", "Active", "kh43" },
                    { 44, "123 Đường 44, TP.HCM", new DateTime(1993, 6, 24), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phan Thị Huệ", "kh44@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0934343434", "70000", "Active", "kh44" },
                    { 45, "123 Đường 45, TP.HCM", new DateTime(1990, 10, 3), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Lý Văn Tân", "kh45@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0935353535", "70000", "Active", "kh45" },
                    { 46, "123 Đường 46, TP.HCM", new DateTime(1996, 4, 29), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đinh Thị Huỳnh Mai", "kh46@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0936363636", "70000", "Active", "kh46" },
                    { 47, "123 Đường 47, TP.HCM", new DateTime(1992, 9, 10), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Tôn Văn Đức", "kh47@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0937373737", "70000", "Active", "kh47" },
                    { 48, "123 Đường 48, TP.HCM", new DateTime(1994, 2, 15), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Phan Thị Thanh Xuân", "kh48@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0938383838", "70000", "Active", "kh48" },
                    { 49, "123 Đường 49, TP.HCM", new DateTime(1989, 7, 21), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Đỗ Văn Phong", "kh49@gmail.com", "Nam", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0939393939", "70000", "Active", "kh49" },
                    { 50, "123 Đường 50, TP.HCM", new DateTime(1997, 12, 5), "TP.HCM", new DateTime(2025, 11, 24, 14, 10, 19), "Vũ Thị Hương Trang", "kh50@gmail.com", "Nữ", null, null, "j2i+6ZQwCH+qAj7J2C3s7Y5k0M8nP1qR4sT7uV9wX2yZ5aB8cD1e4F7g0H3i6J9k", "0940404040", "70000", "Active", "kh50" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all 50 Products
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Products",
                    keyColumn: "ProductId",
                    keyValue: i);
            }

            // Delete all 50 Customers
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Customers",
                    keyColumn: "CustomerId",
                    keyValue: i);
            }
        }
    }
}
