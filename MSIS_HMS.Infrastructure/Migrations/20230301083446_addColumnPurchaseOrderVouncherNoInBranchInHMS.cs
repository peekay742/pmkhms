using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS_HMS.Infrastructure.Migrations
{
    public partial class addColumnPurchaseOrderVouncherNoInBranchInHMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseVoucherFormatForPurchaseOrder",
                table: "Branch",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherFormatForPurchaseOrder",
                table: "Branch",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VoucherNoForPurchaseOrder",
                table: "Branch",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseVoucherFormatForPurchaseOrder",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "VoucherFormatForPurchaseOrder",
                table: "Branch");

            migrationBuilder.DropColumn(
                name: "VoucherNoForPurchaseOrder",
                table: "Branch");
        }
    }
}
