USE AASTHA2
TRUNCATE TABLE [dbo].[Appointments] 
TRUNCATE TABLE[dbo].[Charges] 
TRUNCATE TABLE[dbo].[Deliveries] 
TRUNCATE TABLE[dbo].[Operations] 
TRUNCATE TABLE[dbo].[IpdLookups] 
TRUNCATE TABLE[dbo].[Opds] 
DELETE FROM[dbo].[Ipds] DBCC CHECKIDENT('dbo.Ipds', RESEED, 0) 
DELETE FROM[dbo].[Patients] DBCC CHECKIDENT('dbo.Patients', RESEED, 0) 
DELETE FROM[dbo].[Lookups] DBCC CHECKIDENT('dbo.Lookups', RESEED, 0) 