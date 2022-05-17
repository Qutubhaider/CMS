CREATE TABLE tblCategoryMaster
(
    inCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    unCategoryId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inParentCategoryId INT,
    inDepartmentId INT,
    inStatus INT,
    stCategoryName NVARCHAR(200),
	flgIsDeleted BIT,
    dtCreatedDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)