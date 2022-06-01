-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getAlmirahDetail(
@unAlmirahId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inAlmirahId,unAlmirahId,stAlmirahNumber,inZoneId,inDivisionId,inDepartmentId,inStoreId,inRoomId FROM tblAlmirah
  WHERE unAlmirahId=@unAlmirahId
END
