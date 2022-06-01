-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================
--EXEC getDivisionDropDown
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getRoomDropDown
(   
    @inStore INT
)
AS
BEGIN
SELECT inRoomId as id , stRoomNumber as value 
       FROM tblRoom       
       WHERE inStoreId=@inStore
END
