using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addcolumnLabprofileInLabtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabProfileId",
                table: "LabTest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTest_LabProfileId",
                table: "LabTest",
                column: "LabProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTest_LabProfile_LabProfileId",
                table: "LabTest",
                column: "LabProfileId",
                principalTable: "LabProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTest_LabProfile_LabProfileId",
                table: "LabTest");

            migrationBuilder.DropIndex(
                name: "IX_LabTest_LabProfileId",
                table: "LabTest");

            migrationBuilder.DropColumn(
                name: "LabProfileId",
                table: "LabTest");
        }
    }
}
