-- =============================================  
-- Author:   Vaibhav Singh 
-- Create Date: 6-mar-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/  


CREATE PROCEDURE getZoneDropDown
AS
BEGIN
SELECT inZoneId as id , stZoneName as value 
       FROM tblZone

END