using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addColumnFloorInIPDRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "IPDRecord",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPDRecord_FloorId",
                table: "IPDRecord",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_IPDRecord_Floor_FloorId",
                table: "IPDRecord",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IPDRecord_Floor_FloorId",
                table: "IPDRecord");

            migrationBuilder.DropIndex(
                name: "IX_IPDRecord_FloorId",
                table: "IPDRecord");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "IPDRecord");
        }
    }
}
