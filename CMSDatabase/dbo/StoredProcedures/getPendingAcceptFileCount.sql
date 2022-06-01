CREATE PROCEDURE [dbo].[getPendingAcceptFileCount]
    @inUserId int ,
	@inRoleId int,
	@inPendingAcceptFileCount int OUT
AS
BEGIN
   SET @inPendingAcceptFileCount=0;
	SELECT @inPendingAcceptFileCount=COUNT(*) 
	FROM tblStoreFileDetails
	WHERE inCreatedBy=@inUserId AND inStatus=0
END
