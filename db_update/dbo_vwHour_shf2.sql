USE [HTSdb]
GO

/****** Object:  View [dbo].[vwHour_shf2]    Script Date: 09/28/2014 06:59:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[vwHour_shf2]
AS
SELECT dbo.fn_fullHour(CONVERT(datetime, CAST('1/1/1900 ' as varchar(11)) 
	+ CAST(number as varchar(2)) 
	+ CAST(':00:00' as varchar(6)))) dt,
	number - 5 as idx,
	Cast(number as varchar(2)) + ':00:00' as _hour
FROM master..spt_values
WHERE (Type = 'P') and (number between 18 and 23)
			
UNION ALL			
SELECT dbo.fn_fullHour(CONVERT(datetime, CAST('1/1/1900 ' as varchar(11)) 
	+ CAST(number as varchar(2)) 
	+ CAST(':00:00' as varchar(6)))) dt,
	number + 19 as idx,
	Cast(number as varchar(2)) + ':00:00' as _hour
FROM master..spt_values
WHERE (Type = 'P') and (number between 0 and 5) 


GO


