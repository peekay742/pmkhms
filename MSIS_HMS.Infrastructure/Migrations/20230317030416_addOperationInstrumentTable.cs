using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addOperationInstrumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Fee",
                table: "Instrument",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "OperationInstrument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationTreaterId = table.Column<int>(nullable: false),
                    InstrumentId = table.Column<int>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    IsFOC = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationInstrument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationInstrument_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationInstrument_OperationTreater_OperationTreaterId",
                        column: x => x.OperationTreaterId,
                        principalTable: "OperationTreater",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationInstrument_InstrumentId",
                table: "OperationInstrument",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationInstrument_OperationTreaterId",
                table: "OperationInstrument",
                column: "OperationTreaterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationInstrument");

            migrationBuilder.AlterColumn<bool>(
                name: "Fee",
                table: "Instrument",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
