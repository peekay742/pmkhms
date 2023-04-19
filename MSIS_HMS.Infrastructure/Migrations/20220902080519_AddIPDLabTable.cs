using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddIPDLabTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPDLab",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    LabOrderId = table.Column<int>(nullable: false),
                    IPDRecordId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPDLab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPDLab_IPDRecord_IPDRecordId",
                        column: x => x.IPDRecordId,
                        principalTable: "IPDRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IPDLab_LabOrder_LabOrderId",
                        column: x => x.LabOrderId,
                        principalTable: "LabOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPDLab_IPDRecordId",
                table: "IPDLab",
                column: "IPDRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDLab_LabOrderId",
                table: "IPDLab",
                column: "LabOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPDLab");
        }
    }
}
