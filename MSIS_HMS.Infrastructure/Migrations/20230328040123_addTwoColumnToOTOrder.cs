using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addTwoColumnToOTOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cancelled",
                table: "OperationOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForCancellation",
                table: "OperationOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OperationOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "OperationOrder");

            migrationBuilder.DropColumn(
                name: "ReasonForCancellation",
                table: "OperationOrder");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OperationOrder");
        }
    }
}
