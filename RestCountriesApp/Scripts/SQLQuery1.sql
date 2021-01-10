
IF DB_ID(N'WPFProjectDB') IS NOT NULL
BEGIN
	DROP DATABASE [WPFProjectDB]
END


CREATE DATABASE [WPFProjectDB]
GO
USE [WPFProjectDB]
GO

CREATE TABLE [Countries]
(
	[Id] INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(300) NOT NULL,
	[ShortCode] NVARCHAR(4) NOT NULL,
	[Capital] NVARCHAR(100),
	[Region] NVARCHAR(100),
	[Population] BIGINT,
	[FlagBase64] NVARCHAR(MAX),
	[FlagPath] NVARCHAR(200)
)

-- Test your DB

--SELECT * FROM [Countries]

--INSERT INTO [Countries]
--VALUES
--(1, 'Kazakhstan', 'KAZ', 'Nur-sultan', 'Central Asia', 18000000, 'AINEOCN3O3E9WNF92NWDOD', 'C:/images/kaz.svg');

-- DELETE FROM [Countries]

--SELECT MAX(Id) FROM [Countries];
