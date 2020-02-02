using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class sp_patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Sp_GetCollection",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Sp_GetCollection");
        }
    }
}
