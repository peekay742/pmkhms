using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class removeColumnInMedicalRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BPDiastolic",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "BPSystolic",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "GCS",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "Pulse",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "RespiratoryRate",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "SPO2",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "MedicalRecord");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "MedicalRecord");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BPDiastolic",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BPSystolic",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GCS",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pulse",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RespiratoryRate",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPO2",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                table: "MedicalRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
