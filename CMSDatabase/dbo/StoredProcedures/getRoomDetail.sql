-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getRoomDetail(
@unRoomId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inRoomId,unRoomId,stRoomNumber,inZoneId,inDivisionId,inDepartmentId,inStoreId FROM tblRoom
  WHERE unRoomId=@unRoomId
END