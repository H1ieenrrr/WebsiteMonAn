using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMonAn.Migrations
{
    public partial class User01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DienThoai",
                table: "Users",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "GiaMonAn",
                table: "CartDetails",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DienThoai",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GiaMonAn",
                table: "CartDetails");
        }
    }
}
