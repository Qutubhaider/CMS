-- =============================================  
-- Author:   Vaibhav Singh 
-- Create Date: 6-mar-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  


CREATE PROCEDURE getZoneDetail(
@unZoneId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inZoneId,unZoneId,stZoneName FROM tblZone
  WHERE unZoneId=@unZoneId
END