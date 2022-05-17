CREATE TABLE [dbo].[tblDepartment]
(
    inDepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    unDepartmentId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    stDepartmentName NVARCHAR(200) NOT NULL,
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)