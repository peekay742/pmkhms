using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addColumnAndTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                table: "LabResult",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "LabResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiseaseName",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiseaseSummary",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalTreatment",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotographicExaminationAnswer",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LabProfile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabProfile_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabProfile_BranchId",
                table: "LabProfile",
                column: "BranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabProfile");

            migrationBuilder.DropColumn(
                name: "ApproveDate",
                table: "LabResult");

            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "LabResult");

            migrationBuilder.DropColumn(
                name: "DiseaseName",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "DiseaseSummary",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "MedicalTreatment",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "PhotographicExaminationAnswer",
                table: "IPDRecord");
        }
    }
}
