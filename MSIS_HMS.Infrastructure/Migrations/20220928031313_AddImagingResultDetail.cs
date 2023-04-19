using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class AddImagingResultDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagingResultDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ImagingResultId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    AttachmentPath = table.Column<string>(nullable: true),
                    IsTitle = table.Column<bool>(nullable: false),
                    IsPrinted = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    LabTestId = table.Column<int>(nullable: false),
                    TechnicianId = table.Column<int>(nullable: true),
                    TechnicianFee = table.Column<decimal>(nullable: false),
                    TechnicianFeeType = table.Column<int>(nullable: false),
                    ConsultantId = table.Column<int>(nullable: true),
                    ConsultantFee = table.Column<decimal>(nullable: false),
                    ConsultantFeeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingResultDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagingResultDetail_LabPerson_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "LabPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResultDetail_ImagingResult_ImagingResultId",
                        column: x => x.ImagingResultId,
                        principalTable: "ImagingResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResultDetail_LabTest_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImagingResultDetail_LabPerson_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "LabPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResultDetail_ConsultantId",
                table: "ImagingResultDetail",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResultDetail_ImagingResultId",
                table: "ImagingResultDetail",
                column: "ImagingResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResultDetail_LabTestId",
                table: "ImagingResultDetail",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingResultDetail_TechnicianId",
                table: "ImagingResultDetail",
                column: "TechnicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagingResultDetail");
        }
    }
}
