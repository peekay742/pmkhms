using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddNewTableCollectionGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionGroupId",
                table: "LabTest",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollectionGroup",
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
                    table.PrimaryKey("PK_CollectionGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionGroup_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTest_CollectionGroupId",
                table: "LabTest",
                column: "CollectionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionGroup_BranchId",
                table: "CollectionGroup",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabTest_CollectionGroup_CollectionGroupId",
                table: "LabTest",
                column: "CollectionGroupId",
                principalTable: "CollectionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabTest_CollectionGroup_CollectionGroupId",
                table: "LabTest");

            migrationBuilder.DropTable(
                name: "CollectionGroup");

            migrationBuilder.DropIndex(
                name: "IX_LabTest_CollectionGroupId",
                table: "LabTest");

            migrationBuilder.DropColumn(
                name: "CollectionGroupId",
                table: "LabTest");
        }
    }
}
