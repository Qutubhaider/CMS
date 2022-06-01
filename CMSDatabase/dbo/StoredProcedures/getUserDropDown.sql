-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getUserDropDown
AS
BEGIN
SELECT inUserProfileId as id , stFirstName+' '+stLastName as value 
       FROM tblUserProfile
END