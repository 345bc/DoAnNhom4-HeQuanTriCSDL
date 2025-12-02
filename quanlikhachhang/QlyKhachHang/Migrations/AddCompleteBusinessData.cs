using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QlyKhachHang.Migrations
{
    /// <inheritdoc />
    public partial class AddCompleteBusinessData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert 50 Invoices (từ Orders data)
            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "InvoiceNumber", "InvoiceDate", "SubTotal", "TotalAmount", "ShippingAddress", "Status", "Notes", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, 1, "ORD001", new DateTime(2025, 11, 20, 10, 30, 0), 500000m, 550000m, "123 Đường Nguyễn Huệ, Q1, TP HCM", "Đã giao", "", new DateTime(2025, 11, 20, 10, 30, 0), null },
                    { 2, 2, "ORD002", new DateTime(2025, 11, 20, 11, 0, 0), 750000m, 825000m, "456 Đường Lê Lợi, Q1, TP HCM", "Đã giao", "", new DateTime(2025, 11, 20, 11, 0, 0), null },
                    { 3, 3, "ORD003", new DateTime(2025, 11, 20, 12, 0, 0), 600000m, 660000m, "789 Đường Trần Hưng Đạo, Q5, TP HCM", "Đang giao", "", new DateTime(2025, 11, 20, 12, 0, 0), null },
                    { 4, 4, "ORD004", new DateTime(2025, 11, 20, 13, 30, 0), 450000m, 495000m, "321 Đường Võ Văn Kiệt, Q3, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 20, 13, 30, 0), null },
                    { 5, 5, "ORD005", new DateTime(2025, 11, 20, 14, 0, 0), 1000000m, 1100000m, "654 Đường Nguyễn Hữu Cảnh, Bình Thạnh, TP HCM", "Đã giao", "", new DateTime(2025, 11, 20, 14, 0, 0), null },
                    { 6, 6, "ORD006", new DateTime(2025, 11, 20, 15, 0, 0), 350000m, 350000m, "987 Đường Phạm Văn Đồng, Thủ Đức, TP HCM", "Hủy", "", new DateTime(2025, 11, 20, 15, 0, 0), null },
                    { 7, 7, "ORD007", new DateTime(2025, 11, 20, 16, 0, 0), 800000m, 880000m, "147 Đường Cách Mạng Tháng 8, Q3, TP HCM", "Đã giao", "", new DateTime(2025, 11, 20, 16, 0, 0), null },
                    { 8, 8, "ORD008", new DateTime(2025, 11, 20, 17, 0, 0), 550000m, 605000m, "258 Đường Bà Triệu, Quận 1, TP HCM", "Đang giao", "", new DateTime(2025, 11, 20, 17, 0, 0), null },
                    { 9, 9, "ORD009", new DateTime(2025, 11, 20, 18, 0, 0), 920000m, 1012000m, "369 Đường Tôn Đức Thắng, Q1, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 20, 18, 0, 0), null },
                    { 10, 10, "ORD010", new DateTime(2025, 11, 20, 19, 0, 0), 700000m, 770000m, "741 Đường Nguyễn Thị Minh Khai, Q3, TP HCM", "Đã giao", "", new DateTime(2025, 11, 20, 19, 0, 0), null },
                    { 11, 11, "ORD011", new DateTime(2025, 11, 21, 8, 0, 0), 450000m, 495000m, "852 Đường Đinh Tiên Hoàng, Q1, TP HCM", "Đã giao", "", new DateTime(2025, 11, 21, 8, 0, 0), null },
                    { 12, 12, "ORD012", new DateTime(2025, 11, 21, 9, 0, 0), 630000m, 693000m, "963 Đường Nguyễn Ái Quốc, Gò Vấp, TP HCM", "Đang giao", "", new DateTime(2025, 11, 21, 9, 0, 0), null },
                    { 13, 13, "ORD013", new DateTime(2025, 11, 21, 10, 0, 0), 580000m, 638000m, "159 Đường Mạc Đĩnh Chi, Q1, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 21, 10, 0, 0), null },
                    { 14, 14, "ORD014", new DateTime(2025, 11, 21, 11, 0, 0), 420000m, 420000m, "357 Đường Trịnh Đình Trọng, Phú Nhuận, TP HCM", "Hủy", "", new DateTime(2025, 11, 21, 11, 0, 0), null },
                    { 15, 15, "ORD015", new DateTime(2025, 11, 21, 12, 0, 0), 890000m, 979000m, "654 Đường Lê Văn Sỹ, Phú Nhuận, TP HCM", "Đã giao", "", new DateTime(2025, 11, 21, 12, 0, 0), null },
                    { 16, 16, "ORD016", new DateTime(2025, 11, 21, 13, 0, 0), 560000m, 616000m, "741 Đường Phạm Hồng Thái, Q5, TP HCM", "Đã giao", "", new DateTime(2025, 11, 21, 13, 0, 0), null },
                    { 17, 17, "ORD017", new DateTime(2025, 11, 21, 14, 0, 0), 720000m, 792000m, "852 Đường Nguyễn Thành Ý, Q6, TP HCM", "Đang giao", "", new DateTime(2025, 11, 21, 14, 0, 0), null },
                    { 18, 18, "ORD018", new DateTime(2025, 11, 21, 15, 0, 0), 480000m, 528000m, "963 Đường Hùng Vương, Q5, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 21, 15, 0, 0), null },
                    { 19, 19, "ORD019", new DateTime(2025, 11, 21, 16, 0, 0), 650000m, 715000m, "159 Đường Nguyễn Tất Thành, Q4, TP HCM", "Đã giao", "", new DateTime(2025, 11, 21, 16, 0, 0), null },
                    { 20, 20, "ORD020", new DateTime(2025, 11, 21, 17, 0, 0), 510000m, 561000m, "357 Đường Xô Viết Nghệ Tĩnh, Bình Thạnh, TP HCM", "Đã giao", "", new DateTime(2025, 11, 21, 17, 0, 0), null },
                    { 21, 21, "ORD021", new DateTime(2025, 11, 22, 8, 0, 0), 780000m, 858000m, "456 Đường Nước Ngầm, Q1, TP HCM", "Đang giao", "", new DateTime(2025, 11, 22, 8, 0, 0), null },
                    { 22, 22, "ORD022", new DateTime(2025, 11, 22, 9, 0, 0), 390000m, 390000m, "567 Đường Sư Vạn Hạnh, Q10, TP HCM", "Hủy", "", new DateTime(2025, 11, 22, 9, 0, 0), null },
                    { 23, 23, "ORD023", new DateTime(2025, 11, 22, 10, 0, 0), 850000m, 935000m, "678 Đường Hoàng Văn Thụ, Phú Nhuận, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 22, 10, 0, 0), null },
                    { 24, 24, "ORD024", new DateTime(2025, 11, 22, 11, 0, 0), 620000m, 682000m, "789 Đường Tạ Quang Bửu, Q8, TP HCM", "Đã giao", "", new DateTime(2025, 11, 22, 11, 0, 0), null },
                    { 25, 25, "ORD025", new DateTime(2025, 11, 22, 12, 0, 0), 740000m, 814000m, "891 Đường Ung Văn Khiêm, Bình Thạnh, TP HCM", "Đã giao", "", new DateTime(2025, 11, 22, 12, 0, 0), null },
                    { 26, 1, "ORD026", new DateTime(2025, 11, 22, 13, 0, 0), 500000m, 550000m, "123 Đường Khánh Hội, Q4, TP HCM", "Đang giao", "", new DateTime(2025, 11, 22, 13, 0, 0), null },
                    { 27, 2, "ORD027", new DateTime(2025, 11, 22, 14, 0, 0), 610000m, 671000m, "234 Đường Thạch Thị Thanh, Q1, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 22, 14, 0, 0), null },
                    { 28, 3, "ORD028", new DateTime(2025, 11, 22, 15, 0, 0), 930000m, 1023000m, "345 Đường Nguyễn Trường Tộ, Q4, TP HCM", "Đã giao", "", new DateTime(2025, 11, 22, 15, 0, 0), null },
                    { 29, 4, "ORD029", new DateTime(2025, 11, 22, 16, 0, 0), 440000m, 440000m, "456 Đường Lý Chính Thắng, Q3, TP HCM", "Hủy", "", new DateTime(2025, 11, 22, 16, 0, 0), null },
                    { 30, 5, "ORD030", new DateTime(2025, 11, 22, 17, 0, 0), 560000m, 616000m, "567 Đường Hàng Dầu, Q1, TP HCM", "Đã giao", "", new DateTime(2025, 11, 22, 17, 0, 0), null },
                    { 31, 6, "ORD031", new DateTime(2025, 11, 23, 8, 0, 0), 680000m, 748000m, "678 Đường Trần Nhân Tôn, Q5, TP HCM", "Đang giao", "", new DateTime(2025, 11, 23, 8, 0, 0), null },
                    { 32, 7, "ORD032", new DateTime(2025, 11, 23, 9, 0, 0), 520000m, 572000m, "789 Đường Nguyễn Chí Thanh, Q5, TP HCM", "Chờ xác nhận", "", new DateTime(2025, 11, 23, 9, 0, 0), null },
                    { 33, 8, "ORD033", new DateTime(2025, 11, 23, 10, 0, 0), 750000m, 825000m, "891 Đường Trần Quốc Hoàn, Cầu Giấy, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 23, 10, 0, 0), null },
                    { 34, 9, "ORD034", new DateTime(2025, 11, 23, 11, 0, 0), 430000m, 473000m, "123 Phố Bà Triệu, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 23, 11, 0, 0), null },
                    { 35, 10, "ORD035", new DateTime(2025, 11, 23, 12, 0, 0), 590000m, 649000m, "234 Phố Tạ Quang Bửu, Hai Bà Trưng, Hà Nội", "Đang giao", "", new DateTime(2025, 11, 23, 12, 0, 0), null },
                    { 36, 11, "ORD036", new DateTime(2025, 11, 23, 13, 0, 0), 360000m, 360000m, "345 Phố Lê Lai, Hoàn Kiếm, Hà Nội", "Hủy", "", new DateTime(2025, 11, 23, 13, 0, 0), null },
                    { 37, 12, "ORD037", new DateTime(2025, 11, 23, 14, 0, 0), 820000m, 902000m, "456 Phố Phùng Hưng, Hoàn Kiếm, Hà Nội", "Chờ xác nhận", "", new DateTime(2025, 11, 23, 14, 0, 0), null },
                    { 38, 13, "ORD038", new DateTime(2025, 11, 23, 15, 0, 0), 470000m, 517000m, "567 Phố Hàng Gai, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 23, 15, 0, 0), null },
                    { 39, 14, "ORD039", new DateTime(2025, 11, 23, 16, 0, 0), 700000m, 770000m, "678 Phố Thanh Nên, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 23, 16, 0, 0), null },
                    { 40, 15, "ORD040", new DateTime(2025, 11, 23, 17, 0, 0), 540000m, 594000m, "789 Phố Nguyễn Du, Hoàn Kiếm, Hà Nội", "Đang giao", "", new DateTime(2025, 11, 23, 17, 0, 0), null },
                    { 41, 16, "ORD041", new DateTime(2025, 11, 24, 8, 0, 0), 880000m, 968000m, "891 Phố Hàng Bạc, Hoàn Kiếm, Hà Nội", "Chờ xác nhận", "", new DateTime(2025, 11, 24, 8, 0, 0), null },
                    { 42, 17, "ORD042", new DateTime(2025, 11, 24, 9, 0, 0), 420000m, 462000m, "123 Phố Nhà Chung, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 24, 9, 0, 0), null },
                    { 43, 18, "ORD043", new DateTime(2025, 11, 24, 10, 0, 0), 650000m, 650000m, "234 Phố Quan Sứ, Hoàn Kiếm, Hà Nội", "Hủy", "", new DateTime(2025, 11, 24, 10, 0, 0), null },
                    { 44, 19, "ORD044", new DateTime(2025, 11, 24, 11, 0, 0), 760000m, 836000m, "345 Phố Mã Mây, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 24, 11, 0, 0), null },
                    { 45, 20, "ORD045", new DateTime(2025, 11, 24, 12, 0, 0), 520000m, 572000m, "456 Phố Chợ Gạo, Hoàn Kiếm, Hà Nội", "Đang giao", "", new DateTime(2025, 11, 24, 12, 0, 0), null },
                    { 46, 21, "ORD046", new DateTime(2025, 11, 24, 13, 0, 0), 710000m, 781000m, "567 Phố Chợ Dừa, Hoàn Kiếm, Hà Nội", "Chờ xác nhận", "", new DateTime(2025, 11, 24, 13, 0, 0), null },
                    { 47, 22, "ORD047", new DateTime(2025, 11, 24, 14, 0, 0), 380000m, 418000m, "678 Phố Đặng Tinh, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 24, 14, 0, 0), null },
                    { 48, 23, "ORD048", new DateTime(2025, 11, 24, 15, 0, 0), 900000m, 990000m, "789 Phố Lương Văn Can, Hoàn Kiếm, Hà Nội", "Đã giao", "", new DateTime(2025, 11, 24, 15, 0, 0), null },
                    { 49, 24, "ORD049", new DateTime(2025, 11, 24, 16, 0, 0), 490000m, 539000m, "891 Phố Chợ Tôn, Hoàn Kiếm, Hà Nội", "Đang giao", "", new DateTime(2025, 11, 24, 16, 0, 0), null },
                    { 50, 25, "ORD050", new DateTime(2025, 11, 24, 17, 0, 0), 810000m, 810000m, "123 Phố Dã Tượng, Hai Bà Trưng, Hà Nội", "Hủy", "", new DateTime(2025, 11, 24, 17, 0, 0), null }
                });

            // Insert 50 InvoiceDetails
            migrationBuilder.InsertData(
                table: "InvoiceDetails",
                columns: new[] { "InvoiceDetailId", "InvoiceId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 2, 250000m },
                    { 2, 2, 2, 3, 250000m },
                    { 3, 3, 3, 2, 300000m },
                    { 4, 4, 4, 1, 450000m },
                    { 5, 5, 5, 2, 500000m },
                    { 6, 6, 6, 1, 350000m },
                    { 7, 7, 7, 2, 400000m },
                    { 8, 8, 8, 1, 550000m },
                    { 9, 9, 9, 2, 460000m },
                    { 10, 10, 10, 1, 700000m },
                    { 11, 11, 1, 1, 450000m },
                    { 12, 12, 2, 2, 315000m },
                    { 13, 13, 3, 2, 290000m },
                    { 14, 14, 4, 1, 420000m },
                    { 15, 15, 5, 2, 445000m },
                    { 16, 16, 6, 1, 560000m },
                    { 17, 17, 7, 2, 360000m },
                    { 18, 18, 8, 1, 480000m },
                    { 19, 19, 9, 1, 650000m },
                    { 20, 20, 10, 1, 510000m },
                    { 21, 21, 1, 2, 390000m },
                    { 22, 22, 2, 1, 390000m },
                    { 23, 23, 3, 2, 425000m },
                    { 24, 24, 4, 1, 620000m },
                    { 25, 25, 5, 2, 370000m },
                    { 26, 26, 6, 2, 250000m },
                    { 27, 27, 7, 1, 610000m },
                    { 28, 28, 8, 2, 465000m },
                    { 29, 29, 9, 1, 440000m },
                    { 30, 30, 10, 1, 560000m },
                    { 31, 31, 1, 2, 340000m },
                    { 32, 32, 2, 1, 520000m },
                    { 33, 33, 3, 1, 750000m },
                    { 34, 34, 4, 1, 430000m },
                    { 35, 35, 5, 1, 590000m },
                    { 36, 36, 6, 1, 360000m },
                    { 37, 37, 7, 2, 410000m },
                    { 38, 38, 8, 1, 470000m },
                    { 39, 39, 9, 1, 700000m },
                    { 40, 40, 10, 1, 540000m },
                    { 41, 41, 1, 2, 440000m },
                    { 42, 42, 2, 1, 420000m },
                    { 43, 43, 3, 1, 650000m },
                    { 44, 44, 4, 2, 380000m },
                    { 45, 45, 5, 1, 520000m },
                    { 46, 46, 6, 1, 710000m },
                    { 47, 47, 7, 1, 380000m },
                    { 48, 48, 8, 2, 450000m },
                    { 49, 49, 9, 1, 490000m },
                    { 50, 50, 10, 1, 810000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all 50 InvoiceDetails
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.DeleteData(
                    table: "InvoiceDetails",
                    keyColumn: "InvoiceDetailId",
                    keyValue: i);
            }

            // Delete all 50 Invoices
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Invoices",
                    keyColumn: "InvoiceId",
                    keyValue: i);
            }
        }
    }
}
