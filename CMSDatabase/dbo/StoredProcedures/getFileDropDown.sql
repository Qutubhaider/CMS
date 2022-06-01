-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getFileDropDown
AS
BEGIN
SELECT inStoreFileDetailsId as id , (stEmployeeName+' || '+stEmployeeNumber+' || '+stPFNumber+' || '+stPPONumber) as value 
       FROM tblStoreFileDetails
END