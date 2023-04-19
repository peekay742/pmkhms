using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addNewTableOpertionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationOrder",
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
                    PatientId = table.Column<int>(nullable: false),
                    ChiefSurgeonDoctorId = table.Column<int>(nullable: false),
                    OperationRoomId = table.Column<int>(nullable: false),
                    OTDate = table.Column<DateTime>(nullable: false),
                    AdmitDate = table.Column<DateTime>(nullable: false),
                    DoctorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationOrder_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationOrder_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OperationOrder_OperationRoom_OperationRoomId",
                        column: x => x.OperationRoomId,
                        principalTable: "OperationRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationOrder_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationOrder_BranchId",
                table: "OperationOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationOrder_DoctorId",
                table: "OperationOrder",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationOrder_OperationRoomId",
                table: "OperationOrder",
                column: "OperationRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationOrder_PatientId",
                table: "OperationOrder",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationOrder");
        }
    }
}
