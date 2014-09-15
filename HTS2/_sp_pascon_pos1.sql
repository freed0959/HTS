USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[_sp_rtk_pos1]    Script Date: 09/15/2014 20:24:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[_sp_pascon_pos1]
AS

BEGIN
DECLARE @dtm as datetime, 
@tm as datetime = CONVERT(nvarchar, GETDATE(), 108);

--SET @tm = '1900-01-01 09:05:00';

IF(@tm >= '00:00:00' AND @tm < '06:00:00')
BEGIN
	SET @dtm =  CONVERT(nvarchar, DATEADD(Day, -1, GETDATE()), 101)
END
ELSE IF(@tm >= '06:00:00' AND @tm <= '23:59:59')
BEGIN
	SET @dtm =  CONVERT(nvarchar, GETDATE(), 101)	
END

SELECT trd.SourceKode, COALESCE(COUNT(trd.ID_RomDist), 0)  as jumtruck
FROM dbo.tbl_TruckRomDist trd
LEFT OUTER JOIN dbo.tbl_TruckPass p ON trd.ID_RomDist = p.ID_RomDist 
	AND CONVERT(nvarchar, trd.TransactionDate, 101) = CONVERT(nvarchar, p.TransactionDate, 101)
WHERE (CONVERT(nvarchar, trd.TransactionDate, 101) = @dtm)
AND (p.ID_RomDist IS NULL)
GROUP BY trd.SourceKode

END

GO


