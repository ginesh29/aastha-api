using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class fkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Patients_PatientId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_PatientId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<long>(
                name: "IpdId",
                table: "Operations",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "IpdId",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Operations_IpdId",
                table: "Operations",
                column: "IpdId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries",
                column: "IpdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Ipds_IpdId",
                table: "Deliveries",
                column: "IpdId",
                principalTable: "Ipds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Ipds_IpdId",
                table: "Operations",
                column: "IpdId",
                principalTable: "Ipds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Ipds_IpdId",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Ipds_IpdId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_IpdId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "IpdId",
                table: "Operations",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "IpdId",
                table: "Deliveries",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "Deliveries",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_PatientId",
                table: "Deliveries",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Patients_PatientId",
                table: "Deliveries",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
