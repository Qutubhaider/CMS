-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getUserDataByDropDown(
@inUserId INT
)
AS
BEGIN
  SELECT UP.inUserProfileId ,UP.inUserId,UP.stFirstName,UP.stLastName,UP.stEmail,UP.stMobile,UP.stAddress ,U.inRole, D.stDepartmentName from tblUserProfile UP
  JOIN tblUser U ON U.inUserId=UP.inUserId
  JOIN tblDepartment D ON D.inDepartmentId=UP.inDepartmentId
  WHERE inUserProfileId = @inUserId
END
