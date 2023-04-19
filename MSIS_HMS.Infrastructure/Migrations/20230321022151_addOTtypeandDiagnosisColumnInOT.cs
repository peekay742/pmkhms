using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addOTtypeandDiagnosisColumnInOT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiagnosisId",
                table: "OperationTreater",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTType",
                table: "OperationTreater",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationTreater_DiagnosisId",
                table: "OperationTreater",
                column: "DiagnosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTreater_Diagnosis_DiagnosisId",
                table: "OperationTreater",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTreater_Diagnosis_DiagnosisId",
                table: "OperationTreater");

            migrationBuilder.DropIndex(
                name: "IX_OperationTreater_DiagnosisId",
                table: "OperationTreater");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "OperationTreater");

            migrationBuilder.DropColumn(
                name: "OTType",
                table: "OperationTreater");
        }
    }
}
