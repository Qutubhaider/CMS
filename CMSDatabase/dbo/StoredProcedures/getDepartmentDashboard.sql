
--EXEC getDepartmentDashboard 1

CREATE PROC getDepartmentDashboard
(
	@inDepartmentId INT
)
AS
BEGIN
	--SET @inDepartmentId=1;

	SELECT 
		'1'as Id,'Total Operator' as Name,Count(*) inTotalOP
	FROM  tblUserProfile UP
	JOIN tblUser UOP ON UOP.inUserId=UP.inUserId
	WHERE UP.inDepartmentId=@inDepartmentId AND UOP.inRole=5
	
	UNION ALL
	
	SELECT 
		'2'as Id,'Total Reception',Count(*) inTotalR
	FROM  tblUserProfile UP
	JOIN tblUser UR ON UR.inUserId=UP.inUserId
	WHERE UP.inDepartmentId=@inDepartmentId AND UR.inRole=6
	
	UNION ALL

	SELECT 
		'3'as Id,'Total User',Count(*) inTotalR
	FROM  tblUserDetails UP
	JOIN tblUser UR ON UR.inUserId=UP.inUserId
	WHERE UP.inDepartmentId=@inDepartmentId AND UR.inRole=7

	UNION ALL

	SELECT 
		'4'as Id,'Total Complain OWN',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId  AND UR.inRole=4

	UNION ALL

	SELECT 
		'5'as Id,'Total Complain Operator',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=5

	UNION ALL

	SELECT 
		'6'as Id,'Total Complain Reception',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=6

	UNION ALL

	SELECT 
		'7'as Id,'Total Complain User',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId 

	UNION ALL

	SELECT 
		'8'as Id,'Total Pending Complain OWN',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=4 AND IFH.inStatus=1

	UNION ALL

	SELECT 
		'9'as Id,'Total Pending Complain Operator',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=5 AND IFH.inStatus=1

	UNION ALL

	SELECT 
		'10'as Id,'Total Pending Complain Reception',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=6 AND IFH.inStatus=1

	UNION ALL

	SELECT 
		'11'as Id,'Total Forwarded Complain OWN',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=4 AND IFH.inStatus=2

	UNION ALL

	SELECT 
		'12'as Id,'Total Forwarded Complain Operator',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=5 AND IFH.inStatus=2

	UNION ALL

	SELECT 
		'13'as Id,'Total Forwarded Complain Reception',COUNT(*)
	FROM tblIssueFileHistory IFH
	JOIN tblUser UR ON UR.inUserId=IFH.inAssignUserId
	WHERE IFH.inDepartmentId=@inDepartmentId AND UR.inRole=6 AND IFH.inStatus=2

END
