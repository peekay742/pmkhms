using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addThreeColumntoIPDrecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmittedFor",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalOfficer",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service",
                table: "IPDRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmittedFor",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "MedicalOfficer",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "service",
                table: "IPDRecord");
        }
    }
}
