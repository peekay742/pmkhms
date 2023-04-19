using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addCategoryColumnToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Item",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Item");
        }
    }
}
