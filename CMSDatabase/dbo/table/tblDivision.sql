CREATE TABLE [dbo].[tblDivision]
(
    inDivisionId INT IDENTITY(1,1) PRIMARY KEY,
    unDivisionId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    stDivisionName	NVARCHAR(200) NOT NULL,
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)