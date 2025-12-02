using Microsoft.EntityFrameworkCore.Migrations;

namespace QlyKhachHang.Migrations
{
    public partial class AddUsernameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm column Username
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            // Update dữ liệu hiện tại - set Username = Email (không có @)
            migrationBuilder.Sql(
                @"UPDATE Users SET Username = LEFT(Email, CHARINDEX('@', Email) - 1) WHERE Username = ''");

            // Tạo unique index trên Username
            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa index
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            // Xóa column
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");
        }
    }
}
