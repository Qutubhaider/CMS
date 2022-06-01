---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- =============================================  
-- Author: Qutub
-- Create Date: 17-April-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getUserListDepartmentId(
	@inDepartmentId INT
)
AS
BEGIN
SELECT UP.inUserProfileId as id , UP.stFirstName+' '+UP.stLastName as value 
       FROM tblUserProfile UP
	   JOIN tblUser U on U.inUserId = UP.inUserId
       WHERE U.inRole in(4,5) AND UP.inDepartmentId=@inDepartmentId
END