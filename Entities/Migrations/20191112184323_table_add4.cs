using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class table_add4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Appointments_TypeId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TypeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointments");

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TypeId",
                table: "Appointments",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Appointments_TypeId",
                table: "Appointments",
                column: "TypeId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
