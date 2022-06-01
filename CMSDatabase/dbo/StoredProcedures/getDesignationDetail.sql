CREATE PROCEDURE getDesignationDetail(
@unDesignationId UNIQUEIDENTIFIER
)
AS
BEGIN
  SELECT inDesignationId,unDesignationId,stDesignationName,inDivisionId,inZoneId,inDepartmentId FROM tblDesignation
  WHERE unDesignationId=@unDesignationId
END