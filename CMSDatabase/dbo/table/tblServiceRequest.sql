CREATE TABLE [dbo].[tblServiceRequest]
(
	inSRId INT IDENTITY(1,1) PRIMARY KEY,
    unSRId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL, 
    [inStoreFileDetailsId] INT NOT NULL, 
    [dtSRdate] DATETIME NULL, 
    [inCreatedBy] INT NULL,
)
