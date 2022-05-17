CREATE TABLE [dbo].[tblStore]
(
    inStoreId INT IDENTITY(1,1) PRIMARY KEY,
    unStoreId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    stStoreName NVARCHAR(200),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)