using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class lookups1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                   
                    IpdId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    BabyWeight = table.Column<decimal>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IpdDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                   
                    IpdId = table.Column<int>(nullable: false),
                    LookupId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpdDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpdDetail_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IpdDetail_Lookups_LookupId",
                        column: x => x.LookupId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IpdDetail_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),                    
                    IpdId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PatientId = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    ModifiedBy = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CreatedBy",
                table: "Deliveries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ModifiedBy",
                table: "Deliveries",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_PatientId",
                table: "Deliveries",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetail_CreatedBy",
                table: "IpdDetail",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetail_LookupId",
                table: "IpdDetail",
                column: "LookupId");

            migrationBuilder.CreateIndex(
                name: "IX_IpdDetail_ModifiedBy",
                table: "IpdDetail",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_CreatedBy",
                table: "Operations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ModifiedBy",
                table: "Operations",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PatientId",
                table: "Operations",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "IpdDetail");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
