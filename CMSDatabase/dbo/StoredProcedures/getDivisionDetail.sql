-- =============================================  
-- Author: Vaibhav Singh  
-- Create Date: 25-Feb-2022  
-- =============================================  
/*  
Ref# Modified By   Modified date   Description  
*/
CREATE PROCEDURE getDivisionDetail(
@unDivisionId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inDivisionId AS inDivisionId,unDivisionId,inZoneId,stDivisionName AS stDivisionName FROM tblDivision
  WHERE unDivisionId=@unDivisionId
END