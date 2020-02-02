using Microsoft.EntityFrameworkCore.Migrations;

namespace AASTHA2.Entities.Migrations
{
    public partial class sp_ipd_collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetIpdStatistics]                           
                        AS
                        BEGIN
                            SET NOCOUNT ON;
                            SELECT 
							    NEWID() Id,
                                DATEPART(MONTH, DischargeDate)[Month],
	                            DATENAME(MONTH,DischargeDate)[MonthName],
	                            DATEPART(YEAR, DischargeDate)[Year],
	                            COUNT(*)TotalPatient,
	                            SUM(ISNULL(Amount,0) - ISNULL(Discount,0))TotalCollection 
                            FROM 
	                            (SELECT DISTINCT IpdId,SUM(Days*Rate)Amount,IsDeleted  FROM Charges
	                            WHERE IsDeleted IS NULL or IsDeleted=0
	                            GROUP BY IpdId,IsDeleted)charge
	                            INNER JOIN 
	                            (SELECT Id,DischargeDate,Discount FROM Ipds)ipd
	                            ON charge.IpdId = ipd.Id
                            GROUP BY DATEPART(YEAR, DischargeDate),DATEPART(MONTH,  DischargeDate),DATENAME(MONTH,DischargeDate)
                            ORDER BY DATEPART(YEAR, DischargeDate) DESC,DATEPART(MONTH,  DischargeDate)
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = "DROP PROCEDURE IF EXISTS [dbo].[GetIpdStatistics]";
            migrationBuilder.Sql(sp);
        }
    }
}
