-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================
--EXEC getDivisionDropDown
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getDivisionDropDown
(   
    @inZoneId INT
)
AS
BEGIN
SELECT inDivisionId as id , stDivisionName as value 
       FROM tblDivision
       WHERE inZoneId=@inZoneId
END
