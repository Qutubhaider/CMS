CREATE TABLE [dbo].[tblAlmirah]
(
    inAlmirahId INT IDENTITY(1,1) PRIMARY KEY,
    unAlmirahId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inStoreId INT,
    inRoomId INT,
    stAlmirahNumber NVARCHAR(200),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)