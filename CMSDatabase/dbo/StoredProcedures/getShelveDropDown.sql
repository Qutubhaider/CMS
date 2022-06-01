-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getShelveDropDown(
@inAlmirahId INT
)
AS
BEGIN
SELECT inShelveId as id , stShelveNumber as value 
       FROM tblShelve
       WHERE inAlmirahId=@inAlmirahId
END