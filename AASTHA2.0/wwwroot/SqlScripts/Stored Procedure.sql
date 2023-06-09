CREATE PROCEDURE [dbo].[GetIpdStatistics]                           
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DATEPART(MONTH, DischargeDate)[Month],
     DATENAME(MONTH,DischargeDate)[MonthName],
     DATEPART(YEAR, DischargeDate)[Year],
     COUNT(*)TotalPatient,
     SUM(ISNULL(Amount,0) - ISNULL(Discount,0))TotalCollection 
    FROM 
     (SELECT DISTINCT IpdId,SUM(Days*Rate)Amount,IsDeleted  FROM Charges
     WHERE IsDeleted IS NULL or IsDeleted=0
     GROUP BY IpdId,IsDeleted)charge
     INNER JOIN 
     (SELECT Id,DischargeDate,Discount FROM Ipds WHERE IsDeleted IS NULL or IsDeleted=0)ipd
     ON charge.IpdId = ipd.Id
    GROUP BY DATEPART(YEAR, DischargeDate),DATEPART(MONTH,  DischargeDate),DATENAME(MONTH,DischargeDate)
    ORDER BY DATEPART(YEAR, DischargeDate) DESC,DATEPART(MONTH,  DischargeDate)
END
GO
CREATE PROCEDURE [dbo].[GetOpdStatistics]                           
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DATEPART(MONTH,[Date])[Month],
     DATENAME(MONTH,[Date])[MonthName],
     DATEPART(YEAR, [Date])[Year],
     COUNT(*)TotalPatient,
     SUM(ISNULL(ConsultCharge,0) + ISNULL(UsgCharge,0) + ISNULL(UptCharge,0) + ISNULL(InjectionCharge,0) + ISNULL(OtherCharge,0))TotalCollection
    FROM Opds WHERE IsDeleted IS NULL or IsDeleted=0
    GROUP BY DATEPART(YEAR, [Date]),DATEPART(MONTH,  [Date]),DATENAME(MONTH,[Date])
    ORDER BY DATEPART(YEAR, [Date]) DESC,DATEPART(MONTH,  [Date])
END
GO
CREATE PROCEDURE [dbo].[GetPatientStatistics]                           
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
END