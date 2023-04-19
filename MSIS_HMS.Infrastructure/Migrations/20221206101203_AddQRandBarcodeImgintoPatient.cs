using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddQRandBarcodeImgintoPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarCodeImg",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCodeImg",
                table: "Patient",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarCodeImg",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "QRCodeImg",
                table: "Patient");
        }
    }
}
