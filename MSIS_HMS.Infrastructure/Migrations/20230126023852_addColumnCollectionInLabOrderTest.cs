using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addColumnCollectionInLabOrderTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CollectionFee",
                table: "LabOrderTest",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "LabOrderTest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionQty",
                table: "LabOrderTest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabOrderTest_CollectionId",
                table: "LabOrderTest",
                column: "CollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabOrderTest_Collection_CollectionId",
                table: "LabOrderTest",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabOrderTest_Collection_CollectionId",
                table: "LabOrderTest");

            migrationBuilder.DropIndex(
                name: "IX_LabOrderTest_CollectionId",
                table: "LabOrderTest");

            migrationBuilder.DropColumn(
                name: "CollectionFee",
                table: "LabOrderTest");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "LabOrderTest");

            migrationBuilder.DropColumn(
                name: "CollectionQty",
                table: "LabOrderTest");
        }
    }
}
