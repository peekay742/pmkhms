using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class RemoveLabOrderIdandLabResultI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabOrderId",
                table: "ImagingOrderTest");

            migrationBuilder.DropColumn(
                name: "LabResultId",
                table: "ImagingOrderTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabOrderId",
                table: "ImagingOrderTest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LabResultId",
                table: "ImagingOrderTest",
                type: "int",
                nullable: true);
        }
    }
}
