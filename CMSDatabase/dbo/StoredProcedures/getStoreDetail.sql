-- =============================================  
-- Author:   Vaibhav Singh 
-- Create Date: 6-mar-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  


CREATE PROCEDURE getStoreDetail(
@unStoreId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inStoreId,inZoneId,inDepartmentId,inDivisionId,unStoreId,stStoreName FROM tblStore
  WHERE unStoreId=@unStoreId
END