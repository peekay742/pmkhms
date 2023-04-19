﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddTypeintoOperationTreater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "OperationTreater",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "OperationTreater");
        }
    }
}
