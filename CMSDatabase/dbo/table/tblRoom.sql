CREATE TABLE [dbo].[tblRoom]
(
    inRoomId INT IDENTITY(1,1) PRIMARY KEY,
    unRoomId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    [inDepartmentId] INT,
    inStoreId INT,
    stRoomNumber NVARCHAR(200),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)