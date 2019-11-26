using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class removePatientid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_Patients_PatientId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Operations_PatientId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Operations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "Operations",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PatientId",
                table: "Operations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_Patients_PatientId",
                table: "Operations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
