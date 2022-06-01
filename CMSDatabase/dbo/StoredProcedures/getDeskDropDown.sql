-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================
--EXEC getDeskDropDown
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getDeskDropDown
(   
     @inDivisionId INT
)
AS
BEGIN
SELECT inDeskid as id , stDeskName as value 
       FROM tblDeskDetail
       WHERE inDivisionId=@inDivisionId
END
