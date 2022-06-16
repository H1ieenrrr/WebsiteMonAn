using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMonAn.Migrations
{
    public partial class cartdetails01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenMonAn",
                table: "CartDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenMonAn",
                table: "CartDetails");
        }
    }
}
