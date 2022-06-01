-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getDeskDetail(
@unDeskId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inDeskId,unDeskId,stDeskName,inZoneId,inDivisionId,inDepartmentId,inDesignationId,inStatus FROM tblDeskDetail
  WHERE unDeskId=@unDeskId
END