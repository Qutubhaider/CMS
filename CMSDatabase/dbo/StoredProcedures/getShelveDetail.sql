-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 11-MAR-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getShelveDetail(
@unShelveId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inShelveId,unShelveId,stShelveNumber,inZoneId,inDivisionId,inDepartmentId,inStoreId,inRoomId,inAlmirahId FROM tblShelve
  WHERE unShelveId=@unShelveId
END
