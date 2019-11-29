using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class manageDBField2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpdDetails");

            migrationBuilder.CreateTable(
                name: "IpdLookups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                    
                    IpdId = table.Column<long>(nullable: false),
                    LookupId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpdLookups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpdLookups_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IpdLookups_Ipds_IpdId",
                        column: x => x.IpdId,
                        principalTable: "Ipds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpdLookups_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpdLookups_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpdLookups_CreatedBy",
                table: "IpdLookups",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IpdLookups_IpdId",
                table: "IpdLookups",
                column: "IpdId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdLookups_LookupId",
                table: "IpdLookups",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdLookups_ModifiedBy",
                table: "IpdLookups",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpdLookups");

            migrationBuilder.CreateTable(
                name: "IpdDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IpdId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LookupId = table.Column<long>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpdDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpdDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IpdDetails_Ipds_IpdId",
                        column: x => x.IpdId,
                        principalTable: "Ipds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpdDetails_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpdDetails_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetails_CreatedBy",
                table: "IpdDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetails_IpdId",
                table: "IpdDetails",
                column: "IpdId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetails_LookupId",
                table: "IpdDetails",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetails_ModifiedBy",
                table: "IpdDetails",
                column: "ModifiedBy");
        }
    }
}
