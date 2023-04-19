using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addDischargeTypeTableandaddColumnDischargeTypeInIPDRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DischargeTypeId",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DischargeType",
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
                    table.PrimaryKey("PK_DischargeType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargeType_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPDRecord_DischargeTypeId",
                table: "IPDRecord",
                column: "DischargeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargeType_BranchId",
                table: "DischargeType",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_IPDRecord_DischargeType_DischargeTypeId",
                table: "IPDRecord",
                column: "DischargeTypeId",
                principalTable: "DischargeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPDRecord_DischargeType_DischargeTypeId",
                table: "IPDRecord");

            migrationBuilder.DropTable(
                name: "DischargeType");

            migrationBuilder.DropIndex(
                name: "IX_IPDRecord_DischargeTypeId",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "DischargeTypeId",
                table: "IPDRecord");
        }
    }
}
