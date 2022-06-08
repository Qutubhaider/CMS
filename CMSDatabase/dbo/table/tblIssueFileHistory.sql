CREATE TABLE [dbo].[tblIssueFileHistory]
(
    inlssueFileId INT IDENTITY(1,1) PRIMARY KEY,
    unlssueFileId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inAssignUserId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inStoreFileDetailsId INT,
    dtIssueDate DATETIME,
    stComment NVARCHAR(200),
    inStatus INT,
    stFileName NVARCHAR(200),
    stUnFileName NVARCHAR(200),
    flgIsDeleted BIT DEFAULT(0),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL, 
    [inSRId] INT NOT NULL, 
    [inCategoryId] INT NULL, 
    [inSubCategoryId] INT NULL
)