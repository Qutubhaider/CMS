CREATE FUNCTION [dbo].[getDateTimeByTimezone]
( 
	@dtDate DATETIME = NULL,
	@stSourceTimezone  NVARCHAR(256),
	@stDestTimezone  NVARCHAR(256),
	@stOFFSET NVARCHAR(20)
)
RETURNS DATETIME
AS
BEGIN
		DECLARE @retval DATETIME;

		IF(@dtDate IS NULL)
			SELECT 
			 @retval = GETUTCDATE()
  						AT TIME ZONE 'UTC'
						  AT TIME ZONE @stDestTimezone
		ELSE
			SELECT 
			 @retval =  @dtDate
  						AT TIME ZONE @stSourceTimezone
						AT TIME ZONE @stDestTimezone

		RETURN @retval
END