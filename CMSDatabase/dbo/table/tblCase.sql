CREATE TABLE [dbo].[tblCase]
(
	inCaseId INT IDENTITY(1,1) PRIMARY KEY,
    unCaseId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inZoneId	INT NOT NULL,
    inDivisionId	INT NOT NULL,
    inDepartmentId	INT NOT NULL,
    inDesignationId	INT NOT NULL,
    inStoreFileDetailId	INT NOT NULL,
    inStatus	INT NOT NULL,
    stComment	NVARCHAR(200) NOT NULL,
    dtCreateDate DATETIME NOT NULL,
    inAcceptededBy	INT NOT NULL,
	inAssignedBy INT NOT NULL, 
    [inSRId] INT NOT NULL,
)
