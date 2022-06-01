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
  SELECT UP.inUserProfileId ,UP.inUserId,UP.stFirstName,UP.stLastName,UP.stEmail,UP.stMobile,UP.stAddress ,U.inRole from tblUserProfile UP
  JOIN tblUser U ON U.inUserId=UP.inUserId
  WHERE inUserProfileId = @inUserId
END
