CREATE PROCEDURE [dbo].[getStoreUserCount]
	@inUserId int ,
	@inRoleId int,
	@inStoreUserCount int OUT
AS
BEGIN
   SET @inStoreUserCount=0;
	SELECT @inStoreUserCount=COUNT(*) 
	FROM tblUserProfile UP
	JOIN tblUser U ON U.inUserId=UP.inUserId
	WHERE UP.inCreatedBy=@inUserId AND U.inRole=@inRoleId
END
