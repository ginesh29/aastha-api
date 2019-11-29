using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class manageDBField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UsgCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<decimal>(
                name: "UptCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<decimal>(
                name: "InjectionCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<decimal>(
                name: "ConsultCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "IpdId",
                table: "IpdDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetails_IpdId",
                table: "IpdDetails",
                column: "IpdId");

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetails_Ipds_IpdId",
                table: "IpdDetails",
                column: "IpdId",
                principalTable: "Ipds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetails_Ipds_IpdId",
                table: "IpdDetails");

            migrationBuilder.DropIndex(
                name: "IX_IpdDetails_IpdId",
                table: "IpdDetails");

            migrationBuilder.AlterColumn<long>(
                name: "UsgCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "UptCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "OtherCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "InjectionCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<long>(
                name: "ConsultCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "IpdId",
                table: "IpdDetails",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Charges",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
