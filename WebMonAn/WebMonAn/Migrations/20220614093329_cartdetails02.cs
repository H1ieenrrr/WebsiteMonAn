using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMonAn.Migrations
{
    public partial class cartdetails02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hinh",
                table: "CartDetails",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hinh",
                table: "CartDetails");
        }
    }
}
