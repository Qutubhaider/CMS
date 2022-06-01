-- ============================================= 
-- Author:  Qutub Haider 
-- EXEC [getDivisionList]
-- ============================================= 
/* 
Ref#	Modified By			Modified date			Description 
*/ 

CREATE PROCEDURE getDepartmentDetail(
@unDepartmentId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inDepartmentId,unDepartmentId,inZoneId,inDivisionId,stDepartmentName FROM tblDepartment
  WHERE unDepartmentId=@unDepartmentId
END