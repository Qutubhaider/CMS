-- =============================================   
-- Author:   Qutub Haider 
-- Create Date: 20-mar-22   
-- =============================================   
 
CREATE PROCEDURE getUserByEmail( 
	@stUserEmail NVARCHAR(200) 
) 
AS 
BEGIN
	SELECT 
		U.inUserId,U.unUserId,U.inStatus,U.inRole,U.stEmail,U.stMobile,
		U.stPassword,U.stUsername,UP.inDeskid,UP.inDivisionId,
		UP.inDesignationId,UP.inZoneId,UP.inStoreId,UP.inDepartmentId,
		DP.stDepartmentName,UP.stFirstName,Z.stZoneName,stDivisionName
	FROM tblUser U
	JOIN tblUserProfile UP ON UP.inUserId = U.inUserId 
	JOIN tblZone Z ON Z.inZoneId=UP.inZoneId
	JOIN tblDivision D ON D.inDivisionId=UP.inDivisionId
	JOIN tblDepartment DP ON DP.inDepartmentId = UP.inDepartmentId
	WHERE U.stEmail=@stUserEmail 
END 