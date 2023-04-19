using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addServiceChargesandApprovePersonInLabOrderandLabResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedPerson",
                table: "LabResult",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCharges",
                table: "LabOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedPerson",
                table: "LabResult");

            migrationBuilder.DropColumn(
                name: "ServiceCharges",
                table: "LabOrder");
        }
    }
}
