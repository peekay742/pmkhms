using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addColumnGetCollectionInLabOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GetCollection",
                table: "LabOrder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "GetCollectionDate",
                table: "LabOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetCollection",
                table: "LabOrder");

            migrationBuilder.DropColumn(
                name: "GetCollectionDate",
                table: "LabOrder");
        }
    }
}
