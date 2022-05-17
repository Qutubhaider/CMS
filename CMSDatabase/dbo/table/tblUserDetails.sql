CREATE TABLE [dbo].[tblUserDetails]
(
	inUserDetailId INT IDENTITY(1,1) PRIMARY KEY,
    unUserDetailId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inDeskid INT,  --default 0
    inUserId INT,
    inZoneId INT,
    inStoreId INT,   --default 0
    inDivisionId INT,
    inDepartmentId INT,
    inDesignationId INT,
    stFirstName NVARCHAR(200),
    stLastName NVARCHAR(200),
    stEmail NVARCHAR(200),
    stMobile NVARCHAR(200),
    stAddress NVARCHAR(200),  -- add
	inEmployeeType INT,   --add
    stPFNumber NVARCHAR(200),  --add
    stEmployeeNumber NVARCHAR(200), --add
    stPPONumber NVARCHAR(200), --add
    inStatus INT,  --default 1
    flgIsDeleted BIT DEFAULT(1),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy	INT NOT NULL
)
