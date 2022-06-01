-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getUserDetail(
@unUserProfileId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT UP.inUserProfileId,UP.unUserProfileId,U.inUserId,U.inRole,UP.inZoneId,UP.inStoreId,UP.inDivisionId,UP.inDeskid,UP.inDepartmentId,UP.inDesignationId,UP.stFirstName,UP.stLastName,UP.stEmail,UP.stMobile,UP.stAddress,UP.inStatus 
  FROM tblUserProfile UP
  JOIN tblUser U ON U.inUserId=UP.inUserId
  WHERE unUserProfileId=@unUserProfileId
END
