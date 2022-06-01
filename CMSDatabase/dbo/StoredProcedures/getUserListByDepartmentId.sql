-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getUserListByDepartmentId(
@inDivisionId INT
)
AS
BEGIN
SELECT UP.inUserProfileId as id , UP.stFirstName+' '+UP.stLastName as value 
       FROM tblUserProfile UP
	   JOIN tblUser U on U.inUserId = UP.inUserId
       WHERE U.inRole=5  AND UP.inDivisionId=@inDivisionId
END