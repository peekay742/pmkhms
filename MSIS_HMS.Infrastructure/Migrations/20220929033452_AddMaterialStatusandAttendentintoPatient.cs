using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddMaterialStatusandAttendentintoPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attendent",
                table: "Patient",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialStatus",
                table: "Patient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendent",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "MaterialStatus",
                table: "Patient");
        }
    }
}
