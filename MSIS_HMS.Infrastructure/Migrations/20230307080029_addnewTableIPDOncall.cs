using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addnewTableIPDOncall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPDOncall",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    IPDRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPDOncall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPDOncall_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IPDOncall_IPDRecord_IPDRecordId",
                        column: x => x.IPDRecordId,
                        principalTable: "IPDRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPDOncall_DoctorId",
                table: "IPDOncall",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_IPDOncall_IPDRecordId",
                table: "IPDOncall",
                column: "IPDRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPDOncall");
        }
    }
}
