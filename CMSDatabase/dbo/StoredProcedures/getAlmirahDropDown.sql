-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================
--EXEC getDivisionDropDown
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getAlmirahDropDown
(   
    @inRoomId INT
)
AS
BEGIN
SELECT inAlmirahId as id , stAlmirahNumber as value 
       FROM tblAlmirah
       WHERE inRoomId=@inRoomId
END
