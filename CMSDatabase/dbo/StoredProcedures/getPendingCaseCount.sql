CREATE PROCEDURE [dbo].[getPendingCaseCount]
    @inUserId int ,
	@inRoleId int,
	@inPendingCaseCount int OUT
AS
BEGIN
   SET @inPendingCaseCount=0;
	SELECT @inPendingCaseCount=COUNT(*) 
	FROM tblCase
	WHERE inAssignedBy=@inUserId AND inStatus=1
END

