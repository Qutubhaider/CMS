CREATE TABLE [dbo].[tblDesignation]
(
    inDesignationId INT IDENTITY(1,1) PRIMARY KEY,
    unDesignationId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    stDesignationName NVARCHAR(200) NOT NULL,
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)