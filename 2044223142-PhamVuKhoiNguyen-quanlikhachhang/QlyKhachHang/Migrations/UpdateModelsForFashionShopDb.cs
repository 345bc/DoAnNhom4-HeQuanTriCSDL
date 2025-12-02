using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QlyKhachHang.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsForFashionShopDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Products_ProductId",
                table: "InvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

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

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ReviewProduct");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "InvoiceDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "KhachHang");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ReviewProduct",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ReviewProduct",
                newName: "MaKH");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "ReviewProduct",
                newName: "Review_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "ReviewProduct",
                newName: "IX_ReviewProduct_Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "ReviewProduct",
                newName: "IX_ReviewProduct_MaKH");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Product",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Product",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Orders",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "MaKH");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Orders",
                newName: "Order_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_CustomerId",
                table: "Orders",
                newName: "IX_Orders_MaKH");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetail",
                newName: "Product_Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "OrderDetail",
                newName: "Order_Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceDetailId",
                table: "OrderDetail",
                newName: "OrderDetail_Id");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_ProductId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_Order_Id");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "KhachHang",
                newName: "SoDT");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "KhachHang",
                newName: "TenKH");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "KhachHang",
                newName: "NgayDangKy");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "KhachHang",
                newName: "MaKH");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Cart",
                newName: "MaKH");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Cart",
                newName: "Cart_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CustomerId",
                table: "Cart",
                newName: "IX_Cart_MaKH");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Product",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultImageId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductSlug",
                table: "Product",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Product",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoldCount",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "Coupon_Id",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetail",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "OrderDetail",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Product_Id",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewProduct",
                table: "ReviewProduct",
                column: "Review_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Product_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "OrderDetail_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang",
                column: "MaKH");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Cart_Id");

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsageLimitPerUser = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Coupon_Id",
                table: "Orders",
                column: "Coupon_Id");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Email",
                table: "KhachHang",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Username",
                table: "KhachHang",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Product_Id",
                table: "Cart",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_KhachHang_MaKH",
                table: "Cart",
                column: "MaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKH",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Product_Product_Id",
                table: "Cart",
                column: "Product_Id",
                principalTable: "Product",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Orders_Order_Id",
                table: "OrderDetail",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Order_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Product_Product_Id",
                table: "OrderDetail",
                column: "Product_Id",
                principalTable: "Product",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coupon_Coupon_Id",
                table: "Orders",
                column: "Coupon_Id",
                principalTable: "Coupon",
                principalColumn: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_KhachHang_MaKH",
                table: "Orders",
                column: "MaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKH",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewProduct_KhachHang_MaKH",
                table: "ReviewProduct",
                column: "MaKH",
                principalTable: "KhachHang",
                principalColumn: "MaKH",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewProduct_Product_Product_Id",
                table: "ReviewProduct",
                column: "Product_Id",
                principalTable: "Product",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_KhachHang_MaKH",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Product_Product_Id",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Orders_Order_Id",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Product_Product_Id",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coupon_Coupon_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_KhachHang_MaKH",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewProduct_KhachHang_MaKH",
                table: "ReviewProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewProduct_Product_Product_Id",
                table: "ReviewProduct");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewProduct",
                table: "ReviewProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Coupon_Id",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhachHang",
                table: "KhachHang");

            migrationBuilder.DropIndex(
                name: "IX_KhachHang_Email",
                table: "KhachHang");

            migrationBuilder.DropIndex(
                name: "IX_KhachHang_Username",
                table: "KhachHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Product_Id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DefaultImageId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductSlug",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SoldCount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Coupon_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Product_Id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "ReviewProduct",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Invoices");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "KhachHang",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "Reviews",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "MaKH",
                table: "Reviews",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Review_Id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewProduct_Product_Id",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewProduct_MaKH",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "Invoices",
                newName: "InvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Invoices",
                newName: "InvoiceDate");

            migrationBuilder.RenameColumn(
                name: "MaKH",
                table: "Invoices",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "Invoices",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_MaKH",
                table: "Invoices",
                newName: "IX_Invoices_CustomerId");

            migrationBuilder.RenameColumn(
                name: "Product_Id",
                table: "InvoiceDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "InvoiceDetails",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "OrderDetail_Id",
                table: "InvoiceDetails",
                newName: "InvoiceDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_Product_Id",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_Order_Id",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceId");

            migrationBuilder.RenameColumn(
                name: "TenKH",
                table: "Customers",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "SoDT",
                table: "Customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "NgayDangKy",
                table: "Customers",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "MaKH",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "MaKH",
                table: "Carts",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Cart_Id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_MaKH",
                table: "Carts",
                newName: "IX_Carts_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceDetails",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "InvoiceDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "CartId");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "City", "CreatedDate", "CustomerName", "Email", "LastLoginDate", "ModifiedDate", "PasswordHash", "Phone", "PostalCode", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "123 ???ng 1, TP.HCM", "TP.HCM", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2888), "Nguy?n V?n A", "nguyenvana@example.com", null, null, "", "0912345678", "70000", "Active", "" },
                    { 2, "456 ???ng 2, HN", "Hà N?i", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2892), "Tr?n Th? B", "tranthib@example.com", null, null, "", "0987654321", "10000", "Active", "" },
                    { 3, "789 ???ng 3, ?N", "?à N?ng", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2895), "Ph?m V?n C", "phamvanc@example.com", null, null, "", "0901234567", "50000", "Active", "" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "CreatedDate", "Description", "Price", "ProductName", "Stock" },
                values: new object[,]
                {
                    { 1, "Áo", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2615), "Áo thun nam ch?t l??ng cao", 150000m, "Áo Thun Nam", 100 },
                    { 2, "Áo", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2619), "Áo s? mi nam ki?u dáng hi?n ??i", 250000m, "Áo S? Mi Nam", 50 },
                    { 3, "Qu?n", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2622), "Qu?n jeans nam b?n b?", 350000m, "Qu?n Jeans Nam", 75 },
                    { 4, "Áo", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2625), "Áo dài truy?n th?ng sang tr?ng", 450000m, "Áo Dài N?", 40 },
                    { 5, "Váy", new DateTime(2025, 11, 24, 15, 43, 34, 550, DateTimeKind.Local).AddTicks(2680), "Váy n? th?i trang", 300000m, "Váy N?", 60 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Products_ProductId",
                table: "InvoiceDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
