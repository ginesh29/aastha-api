using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class rm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sp_GetCollection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sp_GetCollection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    MonthName = table.Column<string>(nullable: true),
                    TotalCollection = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalPatient = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sp_GetCollection", x => x.Id);
                });
        }
    }
}
