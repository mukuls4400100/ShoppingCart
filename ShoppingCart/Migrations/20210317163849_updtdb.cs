using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Migrations
{
    public partial class updtdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPro",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgCat",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPro",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImgCat",
                table: "Category");
        }
    }
}
