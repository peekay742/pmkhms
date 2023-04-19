using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddImagingOrderandResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagingOrderTest_ImagingOrder_ImagingOrderId",
                table: "ImagingOrderTest");

            migrationBuilder.AlterColumn<int>(
                name: "ImagingOrderId",
                table: "ImagingOrderTest",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImagingOrderTest_ImagingOrder_ImagingOrderId",
                table: "ImagingOrderTest",
                column: "ImagingOrderId",
                principalTable: "ImagingOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagingOrderTest_ImagingOrder_ImagingOrderId",
                table: "ImagingOrderTest");

            migrationBuilder.AlterColumn<int>(
                name: "ImagingOrderId",
                table: "ImagingOrderTest",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ImagingOrderTest_ImagingOrder_ImagingOrderId",
                table: "ImagingOrderTest",
                column: "ImagingOrderId",
                principalTable: "ImagingOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
