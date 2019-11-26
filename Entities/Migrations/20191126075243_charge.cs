using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class charge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                    
                    Days = table.Column<decimal>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    LookupId = table.Column<long>(nullable: false),
                    IpdId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Charges_Ipds_IpdId",
                        column: x => x.IpdId,
                        principalTable: "Ipds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charges_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charges_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_CreatedBy",
                table: "Charges",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_IpdId",
                table: "Charges",
                column: "IpdId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_LookupId",
                table: "Charges",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_ModifiedBy",
                table: "Charges",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charges");
        }
    }
}
