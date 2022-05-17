CREATE TABLE [dbo].[tblUserProfile]
(
    inUserProfileId INT IDENTITY(1,1) PRIMARY KEY,
    unUserProfileId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inDeskid INT,
    inUserId INT,
    inZoneId INT,
    inStoreId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inDesignationId INT,
    stFirstName NVARCHAR(200),
    stLastName NVARCHAR(200),
    stEmail NVARCHAR(200),
    stMobile NVARCHAR(200),
    stAddress NVARCHAR(200),
    inStatus INT,
    flgIsDeleted BIT DEFAULT(1),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)