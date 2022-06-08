

CREATE PROCEDURE getStoreDropDown
(
	@inDivisionId INT
)
AS
BEGIN
	   SELECT inStoreId as id , stStoreName as value 
       FROM tblStore 
	   WHERE inDivisionId=@inDivisionId
END
