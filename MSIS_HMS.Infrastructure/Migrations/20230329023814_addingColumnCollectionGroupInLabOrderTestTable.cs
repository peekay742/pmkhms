using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addingColumnCollectionGroupInLabOrderTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionGroupId",
                table: "LabOrderTest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabOrderTest_CollectionGroupId",
                table: "LabOrderTest",
                column: "CollectionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabOrderTest_CollectionGroup_CollectionGroupId",
                table: "LabOrderTest",
                column: "CollectionGroupId",
                principalTable: "CollectionGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabOrderTest_CollectionGroup_CollectionGroupId",
                table: "LabOrderTest");

            migrationBuilder.DropIndex(
                name: "IX_LabOrderTest_CollectionGroupId",
                table: "LabOrderTest");

            migrationBuilder.DropColumn(
                name: "CollectionGroupId",
                table: "LabOrderTest");
        }
    }
}
