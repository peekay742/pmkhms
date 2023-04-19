using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addPatientVitaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientVital",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(nullable: false),
                    GCS = table.Column<string>(nullable: true),
                    BPSystolic = table.Column<string>(nullable: true),
                    BPDiastolic = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Pulse = table.Column<string>(nullable: true),
                    SPO2 = table.Column<string>(nullable: true),
                    RespiratoryRate = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVital_MedicalRecord_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVital_MedicalRecordId",
                table: "PatientVital",
                column: "MedicalRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientVital");
        }
    }
}
