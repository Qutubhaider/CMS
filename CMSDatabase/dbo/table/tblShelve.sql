CREATE TABLE [dbo].[tblShelve]
(
    inShelveId INT IDENTITY(1,1) PRIMARY KEY,
    unShelveId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inStoreId INT,
    inRoomId INT,
    inAlmirahId INT,
    stShelveNumber NVARCHAR(200),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)