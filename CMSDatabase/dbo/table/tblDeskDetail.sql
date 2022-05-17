CREATE TABLE [dbo].[tblDeskDetail]
(
    inDeskid INT IDENTITY(1,1) PRIMARY KEY,
    unDeskId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inDesignationId INT,
    stDeskName NVARCHAR(200),
    inStatus INT,
    flgIsDeleted BIT DEFAULT(1),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)