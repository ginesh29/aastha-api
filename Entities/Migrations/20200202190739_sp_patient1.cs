using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class sp_patient1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetPatientStatistics]                           
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            SELECT 
	                            NEWID() Id,
                                DATEPART(MONTH, CreatedDate)[Month],
	                            DATENAME(MONTH,CreatedDate)[MonthName],
	                            DATEPART(YEAR, CreatedDate)[Year],
	                            COUNT(*)TotalPatient,
                                0.00 TotalCollection
                            FROM Patients
                            GROUP BY DATEPART(YEAR, CreatedDate),DATEPART(MONTH,  CreatedDate),DATENAME(MONTH,CreatedDate)
                            ORDER BY DATEPART(YEAR, CreatedDate) DESC,DATEPART(MONTH,  CreatedDate)
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = "DROP PROCEDURE IF EXISTS [dbo].[GetPatientStatistics]";
            migrationBuilder.Sql(sp);
        }
    }
}
