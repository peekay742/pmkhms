using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddImaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagingOrder",
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
                    Date = table.Column<DateTime>(nullable: false),
                    VoucherNo = table.Column<string>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    PaidDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingOrder_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingOrder_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagingResult",
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
                    Date = table.Column<DateTime>(nullable: false),
                    ResultNo = table.Column<string>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    LabTestId = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    TechnicianId = table.Column<int>(nullable: true),
                    TechnicianFee = table.Column<decimal>(nullable: false),
                    TechnicianFeeType = table.Column<int>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: true),
                    ConsultantFee = table.Column<decimal>(nullable: false),
                    ConsultantFeeType = table.Column<int>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingResult_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResult_LabPerson_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "LabPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResult_LabTest_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResult_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResult_LabPerson_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "LabPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagingOrderTest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabOrderId = table.Column<int>(nullable: false),
                    LabTestId = table.Column<int>(nullable: false),
                    ReferrerId = table.Column<int>(nullable: true),
                    FeeType = table.Column<int>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    ReferralFee = table.Column<decimal>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    LabResultId = table.Column<int>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    ImagingOrderId = table.Column<int>(nullable: true),
                    ImagingResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingOrderTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingOrderTest_ImagingOrder_ImagingOrderId",
                        column: x => x.ImagingOrderId,
                        principalTable: "ImagingOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingOrderTest_ImagingResult_ImagingResultId",
                        column: x => x.ImagingResultId,
                        principalTable: "ImagingResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingOrderTest_LabTest_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingOrderTest_Referrer_ReferrerId",
                        column: x => x.ReferrerId,
                        principalTable: "Referrer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagingResultDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagingResultId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    AttachmentPath = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingResultDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingResultDetail_ImagingResult_ImagingResultId",
                        column: x => x.ImagingResultId,
                        principalTable: "ImagingResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrder_BranchId",
                table: "ImagingOrder",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrder_PatientId",
                table: "ImagingOrder",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrderTest_ImagingOrderId",
                table: "ImagingOrderTest",
                column: "ImagingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrderTest_ImagingResultId",
                table: "ImagingOrderTest",
                column: "ImagingResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrderTest_LabTestId",
                table: "ImagingOrderTest",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingOrderTest_ReferrerId",
                table: "ImagingOrderTest",
                column: "ReferrerId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResultDetail_ImagingResultId",
                table: "ImagingResultDetail",
                column: "ImagingResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResult_BranchId",
                table: "ImagingResult",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResult_ConsultantId",
                table: "ImagingResult",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResult_LabTestId",
                table: "ImagingResult",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResult_PatientId",
                table: "ImagingResult",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResult_TechnicianId",
                table: "ImagingResult",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagingOrderTest");

            migrationBuilder.DropTable(
                name: "ImagingResultDetail");

            migrationBuilder.DropTable(
                name: "ImagingOrder");

            migrationBuilder.DropTable(
                name: "ImagingResult");
        }
    }
}
