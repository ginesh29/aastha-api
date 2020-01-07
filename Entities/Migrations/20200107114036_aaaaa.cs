using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class aaaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Lookups_AddressId",
                table: "Patients");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Patients",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Lookups_AddressId",
                table: "Patients",
                column: "AddressId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Lookups_AddressId",
                table: "Patients");

            migrationBuilder.AlterColumn<long>(
                name: "AddressId",
                table: "Patients",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Lookups_AddressId",
                table: "Patients",
                column: "AddressId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
