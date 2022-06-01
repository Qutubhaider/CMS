-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================
--EXEC getDeskDropDown
/*  
Ref# Modified By   Modified date   Description  
*/

CREATE PROCEDURE getUserListForIssueFile
(   
     @inStoreId INT,
	 @inDivisionId INT
)
AS
BEGIN
SELECT UP.inUserProfileId as id , UP.stFirstName+' '+UP.stLastName as value 
       FROM tblUserProfile UP
	   JOIN tblUser U on U.inUserId = UP.inUserId
       WHERE inStoreId=@inStoreId and inDivisionId=@inDivisionId and U.inRole=5 -- Desk Operator
END
