using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class sp_opd_collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetOpdStatistics]                           
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            SELECT
                                NEWID() Id,
                                DATEPART(MONTH,[Date])[Month],
	                            DATENAME(MONTH,[Date])[MonthName],
	                            DATEPART(YEAR, [Date])[Year],
	                            COUNT(*)TotalPatient,
	                            SUM(ISNULL(ConsultCharge,0) + ISNULL(UsgCharge,0) + ISNULL(UptCharge,0) + ISNULL(InjectionCharge,0) + ISNULL(OtherCharge,0))TotalCollection
                            FROM Opds
                            GROUP BY DATEPART(YEAR, [Date]),DATEPART(MONTH,  [Date]),DATENAME(MONTH,[Date])
                            ORDER BY DATEPART(YEAR, [Date]) DESC,DATEPART(MONTH,  [Date])
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = "DROP PROCEDURE IF EXISTS [dbo].[GetOpdStatistics]";
            migrationBuilder.Sql(sp);
        }
    }
}
