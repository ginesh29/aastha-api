using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operations_IpdId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries");

            migrationBuilder.AddColumn<long>(
                name: "UniqueId",
                table: "Ipds",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Operations_IpdId",
                table: "Operations",
                column: "IpdId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ipds_UniqueId",
                table: "Ipds",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries",
                column: "IpdId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operations_IpdId",
                table: "Operations");

            migrationBuilder.DropIndex(
                name: "IX_Ipds_UniqueId",
                table: "Ipds");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Ipds");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_IpdId",
                table: "Operations",
                column: "IpdId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_IpdId",
                table: "Deliveries",
                column: "IpdId");
        }
    }
}
