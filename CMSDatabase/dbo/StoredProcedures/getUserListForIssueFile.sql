
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
       WHERE inStoreId=@inStoreId and inDivisionId=@inDivisionId and U.inRole IN(3,4,5,6)
END
