CREATE TABLE [dbo].[tblUser]
(
    inUserId INT IDENTITY(1,1) PRIMARY KEY,
    unUserId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    stUsername NVARCHAR(200),
    stPassword NVARCHAR(200),
    inRole INT,
    stEmail NVARCHAR(200),
    stMobile NVARCHAR(200),
    inStatus INT,
    flgIsDeleted BIT DEFAULT(1),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)