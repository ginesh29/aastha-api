using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class table_add1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lookups_Lookups_ParentId1",
                table: "Lookups");

            migrationBuilder.DropForeignKey(
                name: "FK_Opds_Patients_PatientId1",
                table: "Opds");

            migrationBuilder.DropIndex(
                name: "IX_Opds_PatientId1",
                table: "Opds");

            migrationBuilder.DropIndex(
                name: "IX_Lookups_ParentId1",
                table: "Lookups");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Opds");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "Lookups");

            migrationBuilder.AlterColumn<long>(
                name: "UsgCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "UptCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OtherCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "InjectionCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "ConsultCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "Lookups",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opds_PatientId",
                table: "Opds",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_ParentId",
                table: "Lookups",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lookups_Lookups_ParentId",
                table: "Lookups",
                column: "ParentId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opds_Patients_PatientId",
                table: "Opds",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lookups_Lookups_ParentId",
                table: "Lookups");

            migrationBuilder.DropForeignKey(
                name: "FK_Opds_Patients_PatientId",
                table: "Opds");

            migrationBuilder.DropIndex(
                name: "IX_Opds_PatientId",
                table: "Opds");

            migrationBuilder.DropIndex(
                name: "IX_Lookups_ParentId",
                table: "Lookups");

            migrationBuilder.AlterColumn<int>(
                name: "UsgCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "UptCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Opds",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "OtherCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "InjectionCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "ConsultCharge",
                table: "Opds",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "PatientId1",
                table: "Opds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Lookups",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId1",
                table: "Lookups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opds_PatientId1",
                table: "Opds",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lookups_ParentId1",
                table: "Lookups",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Lookups_Lookups_ParentId1",
                table: "Lookups",
                column: "ParentId1",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opds_Patients_PatientId1",
                table: "Opds",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
