using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class fkey1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetail_Users_CreatedBy",
                table: "IpdDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetail_Lookups_LookupId",
                table: "IpdDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetail_Users_ModifiedBy",
                table: "IpdDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpdDetail",
                table: "IpdDetail");

            migrationBuilder.RenameTable(
                name: "IpdDetail",
                newName: "IpdDetails");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetail_ModifiedBy",
                table: "IpdDetails",
                newName: "IX_IpdDetails_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetail_LookupId",
                table: "IpdDetails",
                newName: "IX_IpdDetails_LookupId");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetail_CreatedBy",
                table: "IpdDetails",
                newName: "IX_IpdDetails_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpdDetails",
                table: "IpdDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetails_Users_CreatedBy",
                table: "IpdDetails",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetails_Lookups_LookupId",
                table: "IpdDetails",
                column: "LookupId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetails_Users_ModifiedBy",
                table: "IpdDetails",
                column: "ModifiedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetails_Users_CreatedBy",
                table: "IpdDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetails_Lookups_LookupId",
                table: "IpdDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_IpdDetails_Users_ModifiedBy",
                table: "IpdDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpdDetails",
                table: "IpdDetails");

            migrationBuilder.RenameTable(
                name: "IpdDetails",
                newName: "IpdDetail");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetails_ModifiedBy",
                table: "IpdDetail",
                newName: "IX_IpdDetail_ModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetails_LookupId",
                table: "IpdDetail",
                newName: "IX_IpdDetail_LookupId");

            migrationBuilder.RenameIndex(
                name: "IX_IpdDetails_CreatedBy",
                table: "IpdDetail",
                newName: "IX_IpdDetail_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpdDetail",
                table: "IpdDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetail_Users_CreatedBy",
                table: "IpdDetail",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetail_Lookups_LookupId",
                table: "IpdDetail",
                column: "LookupId",
                principalTable: "Lookups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IpdDetail_Users_ModifiedBy",
                table: "IpdDetail",
                column: "ModifiedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
