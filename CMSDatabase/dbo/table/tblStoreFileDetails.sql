﻿CREATE TABLE [dbo].[tblStoreFileDetails]
(
    inStoreFileDetailsId INT IDENTITY(1,1) PRIMARY KEY,
    unStoreFileDetailsId UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    inUserId INT,
    inZoneId INT,
    inDivisionId INT,
    inDepartmentId INT,
    inStoreId INT,
    inRoomId INT,
    inAlmirahId INT,
    inShelvesId INT,
    stFileName NVARCHAR(200),
    stUnFileName NVARCHAR(200),
    stEmployeeName NVARCHAR(200),
    inEmployeeType INT,
    stPFNumber NVARCHAR(200),
    stEmployeeNumber NVARCHAR(200),
    stPPONumber NVARCHAR(200),
    stMobile NVARCHAR(20),
    inStatus INT,
    flgIsDeleted BIT DEFAULT(0),
    dtCreateDate DATETIME NOT NULL,
    inCreatedBy INT NOT NULL
)