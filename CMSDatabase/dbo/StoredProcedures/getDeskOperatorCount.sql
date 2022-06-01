CREATE PROCEDURE [dbo].[getDeskOperatorCount]
	@inUserId int ,
	@inRoleId int,
	@inDeskOpCount int OUT
AS
BEGIN
   SET @inDeskOpCount=0;
	SELECT @inDeskOpCount=COUNT(*) 
	FROM tblUserProfile UP
	JOIN tblUser U ON U.inUserId=UP.inUserId
	WHERE UP.inCreatedBy=@inUserId AND U.inRole=@inRoleId
END
